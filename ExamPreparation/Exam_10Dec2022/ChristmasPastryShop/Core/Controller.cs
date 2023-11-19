using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Linq;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private IRepository<IBooth> booths;
        public Controller()
        {
            booths = new BoothRepository();
        }
        public string AddBooth(int capacity)
        {
            int boothId = booths.Models.Count + 1;
            Booth currentBooth = new Booth(boothId, capacity);
            booths.AddModel(currentBooth);
            return $"{string.Format(OutputMessages.NewBoothAdded, currentBooth.BoothId, currentBooth.Capacity)}";

        }
        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            if (cocktailTypeName != "MulledWine" && cocktailTypeName != "Hibernation")
            {
                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }

            if (size != "Large" && size != "Middle" && size != "Small")
            {
                return string.Format(OutputMessages.InvalidCocktailSize, size);
            }

            if (booths.Models.Any(b => b.CocktailMenu.Models.Any(cm => cm.Name == cocktailName && cm.Size == size)))
            {
                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }

            IBooth currentBooth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            if (cocktailTypeName == "MulledWine")
            {
                currentBooth.CocktailMenu.AddModel(new MulledWine(cocktailName, size));
            }
            else if (cocktailTypeName == "Hibernation")
            {
                currentBooth.CocktailMenu.AddModel(new Hibernation(cocktailName, size));
            }

            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }
        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            if (delicacyTypeName != "Stolen" && delicacyTypeName != "Gingerbread")
            {
                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }

            if (booths.Models.Any(b => b.DelicacyMenu.Models.Any(dm => dm.Name == delicacyName)))
            {
                return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyTypeName);
            }

            IBooth currentBooth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            if (delicacyTypeName == "Stolen")
            {
                currentBooth.DelicacyMenu.AddModel(new Stolen(delicacyName));
            }
            else
            {
                currentBooth.DelicacyMenu.AddModel(new Gingerbread(delicacyName));
            }

            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);

        }
        public string ReserveBooth(int countOfPeople)
        {
            IBooth firstAvailableBooth =
                booths.Models
                    .Where(b => b.IsReserved == false && b.Capacity >= countOfPeople)
                    .OrderBy(b => b.Capacity)
                    .ThenByDescending(b => b.BoothId)
                    .FirstOrDefault();

            if (firstAvailableBooth == null)
            {
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }

            firstAvailableBooth.ChangeStatus();
            return string.Format(OutputMessages.BoothReservedSuccessfully, firstAvailableBooth.BoothId, countOfPeople);

        }
        public string TryOrder(int boothId, string order)
        {
            IBooth currentBooth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            string[] orderTokens = order.Split('/', StringSplitOptions.RemoveEmptyEntries);

            string itemTypeName = orderTokens[0];
            string itemName = orderTokens[1];
            int countOfItems = int.Parse(orderTokens[2]);
            string size = orderTokens.Length == 4 ? orderTokens[3] : null;

            //check if given type exists
            Type[] cocktailTypes = typeof(ICocktail).Assembly.GetTypes();
            Type[] delicacyTypes = typeof(IDelicacy).Assembly.GetTypes();
            bool hasType = false;
            foreach (var type in cocktailTypes)
            {
                if (type.IsClass && type.Name.Contains(itemTypeName))
                {
                    hasType = true;
                }
            }
            foreach (var type in delicacyTypes)
            {
                if (type.IsClass && type.Name.Contains(itemTypeName))
                {
                    hasType = true;
                }
            }
            if (!hasType)
            {
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);
            }

            //if (itemTypeName != nameof(MulledWine) &&
            //    itemTypeName != nameof(Hibernation) &&
            //    itemTypeName != nameof(Gingerbread) &&
            //    itemTypeName != nameof(Stolen))
            //{
            //    return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
            //}


            //check if booth repository has given type
            if (!currentBooth.CocktailMenu.Models.Any(c => c.Name == itemName)
                && !currentBooth.DelicacyMenu.Models.Any(c => c.Name == itemName))
            {
                return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
            }

            if (itemTypeName == nameof(Hibernation) || itemTypeName == nameof(MulledWine))
            {
                ICocktail currentCocktail = currentBooth.CocktailMenu.Models
                    .FirstOrDefault(c => c.Name == itemName && c.Size == size);
                if (currentCocktail != null)
                {
                    double currentOrderPrice = currentCocktail.Price * countOfItems;
                    currentBooth.UpdateCurrentBill(currentOrderPrice);
                }
                else
                {
                    return string.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                }
            }
            else
            {
                IDelicacy currentDelicacy = currentBooth.DelicacyMenu.Models
                    .FirstOrDefault(c => c.Name == itemName);
                if (currentDelicacy != null)
                {
                    double currentOrderPrice = currentDelicacy.Price * countOfItems;
                    currentBooth.UpdateCurrentBill(currentOrderPrice);
                }
                else
                {
                    return string.Format(OutputMessages.DelicacyStillNotAdded, size, itemName);
                }
            }

            return string.Format(OutputMessages.SuccessfullyOrdered, boothId, countOfItems, itemName);

        }
        public string LeaveBooth(int boothId)
        {
            IBooth currentBooth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            double bill = currentBooth.CurrentBill;
            currentBooth.Charge();
            currentBooth.ChangeStatus();
            return string.Format(OutputMessages.GetBill, $"{bill:f2}")
                   + Environment.NewLine
                   + string.Format(OutputMessages.BoothIsAvailable, currentBooth.BoothId);
        }
        public string BoothReport(int boothId)
        {
            IBooth currentBooth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            return currentBooth.ToString();
        }
    }

}
