using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using App1.Models;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1.Client
{
    class SQLiteManager
    {
        static object locker = new object();

        SQLiteConnection database;

        public SQLiteManager()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            // create the tables
            database.CreateTable<UserCompte>();
            database.CreateTable<GuideCompte>();
            database.CreateTable<IsConnect>();
        }

        public IsConnect GetConnect()
        {
            lock (locker)
            {
                return database.Table<IsConnect>().FirstOrDefault();

            }
        }

        public int saveEtatConnect(IsConnect item)
        {
            lock (locker)
            {
                if (item.Id != 0)
                {
                    database.Update(item);
                    return item.Id;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        public List<UserCompte> GetUsers()
        {
            lock (locker)
            {
                return (from i in database.Table<UserCompte>() select i).ToList();
            }
        }

        public List<GuideCompte> GetGuides()
        {
            lock (locker)
            {
                return (from i in database.Table<GuideCompte>() select i).ToList();
            }
        }

        public List<UserCompte> GetUserNotDone()
        {
            lock (locker)
            {
                return database.Query<UserCompte>("SELECT * FROM [User] WHERE [Done] = 0");
            }
        }

        public List<GuideCompte> GetGuideNotDone()
        {
            lock (locker)
            {
                return database.Query<GuideCompte>("SELECT * FROM [User] WHERE [Done] = 0");

            }
        }

        public UserCompte GetUser(int id)
        {
            lock (locker)
            {
                return database.Table<UserCompte>().FirstOrDefault(x => x.UserCompteId == id);

            }
        }

        public GuideCompte GetGuide(int id)
        {
            lock (locker)
            {
                return database.Table<GuideCompte>().FirstOrDefault(x => x.GuideCompteId == id);

            }
        }

        public int SaveUser(UserCompte item)
        {
            lock (locker)
            {
                if (item.UserCompteId != 0)
                {
                    database.Update(item);
                    return item.UserCompteId;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        public int SaveGuide(GuideCompte item)
        {
            lock (locker)
            {
                if (item.GuideCompteId != 0)
                {
                    database.Update(item);
                    return item.GuideCompteId;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        public int DeleteUser(int id)
        {
            lock (locker)
            {
                return database.Delete<UserCompte>(id);
            }
        }

        public int DeleteGuide(int id)
        {
            lock (locker)
            {
                return database.Delete<GuideCompte>(id);
            }
        }
    }
}