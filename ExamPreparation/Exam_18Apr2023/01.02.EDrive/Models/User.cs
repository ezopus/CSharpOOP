using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;

namespace EDriveRent.Models
{
    public class User : IUser
    {
        private string firstName;
        private string lastName;
        private string drivingLicenseNumber;
        private double rating;
        private bool isBlocked;

        private const double IncreaseRatingAmount = 0.5;
        private const double DecreaseRatingAmount = 2.0;
        private const double MaxRating = 10d;
        private const double MinRating = 0d;

        public User(string firstName, string lastName, string drivingLicenseNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DrivingLicenseNumber = drivingLicenseNumber;
            rating = 0;
            isBlocked = false;
        }

        public string FirstName
        {
            get => firstName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.FirstNameNull);
                }
                firstName = value;
            }
        }
        public string LastName
        {
            get => lastName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.LastNameNull);
                }
                lastName = value;
            }
        }
        public double Rating
        {
            get => rating;
            private set => rating = value;
        }
        public string DrivingLicenseNumber
        {
            get => drivingLicenseNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.DrivingLicenseRequired);
                }
                drivingLicenseNumber = value;
            }
        }
        public bool IsBlocked
        {
            get => isBlocked;
            private set => isBlocked = value;
        }
        public void IncreaseRating()
        {
            if (Rating <= MaxRating - IncreaseRatingAmount)
            {
                Rating += IncreaseRatingAmount;
            }
            else
            {
                Rating = MaxRating;
            }
        }

        public void DecreaseRating()
        {
            if (Rating > MinRating + DecreaseRatingAmount)
            {
                Rating -= DecreaseRatingAmount;
            }
            else
            {
                Rating = MinRating;
                IsBlocked = true;
            }
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} Driving license: {drivingLicenseNumber} Rating: {rating}";
        }
    }
}
