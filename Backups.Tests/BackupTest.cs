using Backups.Algorithm;
using Backups.Backup;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupTest
    {
        private BackupJop _backup;
        
        [SetUp]
        public void Setup()
        { 
            IStorageAlgorithm algorithm = new SplitStorage();
            _backup = new BackupJop(".", algorithm);
        }

        [Test]
        public void test1()
        {
            const string pathFile = @"C:\Users\dewor\Desktop";
            _backup.AddObject(pathFile, "A");
            _backup.AddObject(pathFile, "B");
            _backup.MakeVirtualRestorePoint();
            
            Assert.AreEqual(_backup.VirtualRestorePoints.Count, 1);
            Assert.AreEqual(_backup.VirtualRestorePoints[0].Files.Count, 2);
            
            _backup.RemoveObject(pathFile,"B");
            _backup.MakeVirtualRestorePoint();
            
            Assert.AreEqual(_backup.VirtualRestorePoints.Count, 2);
            Assert.AreEqual(_backup.VirtualRestorePoints[1].Files.Count, 1);
            
            Assert.True(_backup.CheckVirtualRestorePoint(1, 2));
            Assert.True(_backup.CheckVirtualRestorePoint(2, 1));
        }
    }
}