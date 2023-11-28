using NUnit.Framework;
using System;

namespace SmartDevice.Tests
{
    public class Tests_Device
    {
        [Test]
        public void Device_ConstructorIsWorkingCorrectly()
        {
            int expectedMemoryCapacity = 128;

            Device device = new Device(128);

            Assert.AreEqual(expectedMemoryCapacity, device.MemoryCapacity);
        }
        [Test]
        public void Device_AvailableMemoryGetterIsCorrect()
        {
            int expectedAvailableMemory = 128;
            Device device = new Device(128);

            Assert.AreEqual(expectedAvailableMemory, device.AvailableMemory);
        }
        [Test]
        public void Device_AvailablePhotosGetterIsCorrect()
        {
            int expectedPhotos = 0;
            Device device = new Device(128);

            Assert.AreEqual(expectedPhotos, device.Photos);
        }
        [Test]
        public void Device_AvailableApplicationListGetterIsCorrect()
        {
            int expectedApplicationCount = 0;
            Device device = new Device(128);

            Assert.AreEqual(expectedApplicationCount, device.Applications.Count);
        }

        [Test]
        public void Device_TakePhoto_ReturnsTrueIfPhotoCanBeTaken()
        {
            bool expectedResult = true;
            Device device = new Device(128);

            Assert.AreEqual(expectedResult, device.TakePhoto(1));
            Assert.AreEqual(1, device.Photos);
            Assert.AreEqual(127, device.AvailableMemory);
        }
        [Test]
        public void Device_TakePhoto_ReturnsFalseIfPhotoCanBeTaken()
        {
            bool expectedResult = false;
            Device device = new Device(1);

            Assert.AreEqual(expectedResult, device.TakePhoto(2));
        }

        [Test]
        public void Device_InstallApp_ReturnsCorrectStringAndWorksCorrectly()
        {
            int expectedAvailableMemory = 127;
            string expectedAppName = "Maps";
            Device device = new Device(128);

            Assert.AreEqual($"{expectedAppName} is installed successfully. Run application?", device.InstallApp("Maps", 1));
        }

        [Test]
        public void Device_InstallApp_ThrowsExceptionWhenNoMemoryIsAvailable()
        {
            Device device = new Device(1);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            {
                device.InstallApp("Maps", 10);
            });
        }

        [Test]
        public void Device_FormatDeviceWorksCorrectly()
        {
            Device device = new Device(128);
            device.TakePhoto(1);
            device.TakePhoto(1);
            device.InstallApp("Maps", 1);

            device.FormatDevice();
            Assert.AreEqual(0, device.Photos);
            Assert.AreEqual(0, device.Applications.Count);
            Assert.AreEqual(128, device.AvailableMemory);
        }

        [TestCase(128)]
        public void Device_GetDeviceStatusReturnsCorrectString(int memory)
        {
            int photoSize = 1;
            int appSize = 1;
            string appName = "Maps";
            Device device = new Device(memory);
            device.TakePhoto(photoSize);
            device.InstallApp(appName, appSize);
            string expectedResult =
                $"Memory Capacity: {memory} MB, Available Memory: {memory - photoSize - appSize} MB"
                + Environment.NewLine
                + $"Photos Count: {1}"
                + Environment.NewLine
                + $"Applications Installed: {appName}";

            Assert.AreEqual(expectedResult, device.GetDeviceStatus());

        }
    }
}
