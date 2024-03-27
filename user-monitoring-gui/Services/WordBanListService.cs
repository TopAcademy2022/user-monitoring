﻿using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using user_monitoring_gui.Models;
using user_monitoring_gui.Models.Network;


namespace user_monitoring_gui.Services.Interfaces
{
    /*!
    @brief Class for saving/loading data
    @detailed Performs saving / downloading from a file or remote server
    */
    public class WordBanListService : IWordBanListService
    {
        /* ! interface IServerRequest variable initialization  */

        private IServerRequest _serverReqwest;



        /*!
        @brief  Designer
        @param[in] IServerRequest serverReqwest Сlass instance data    
        */
        public WordBanListService(IServerRequest serverReqwest)
        {
            this._serverReqwest = serverReqwest;
        }

        /*!
        @brief method for saving data
        @param[in] wordBanList data to be saved
        @param[in] dataStorageArea Selecting a storage location option        
        @return returns the true when the data is successfully saved
        */
        public bool Save(WordBanList wordBanList, DataStorageArea dataStorageArea)
        {
            /* ! decoding incoming data into a byte array */

            var bytes = wordBanList.GetWordBanList().Select(i => Encoding.Default.GetBytes($"{i}\n")).ToArray();

            switch (dataStorageArea)
            {
                case DataStorageArea.FILE:
                    using (FileStream fstream = new FileStream("WordBanList.txt", FileMode.Append))
                    {
                        foreach (var item in bytes)
                        {
                            fstream.Write(item, 0, item.Length);
                        }
                    }
                    return true;

                case DataStorageArea.SERVER:
                    foreach (var item in bytes)
                    {
                        this._serverReqwest.SendData(item);
                    }
                    return true;
            }

            return false;
        }

        /*!
        @brief method for loading data        
        @param[in] dataStorageArea Selecting a storage location option        
        @return returns the true when the data is successfully loaded
        */
        public bool Load(WordBanList wordBanList, DataStorageArea dataStorageArea)
        {
            

            switch (dataStorageArea)
            {
                case DataStorageArea.FILE:                  

                    FileInfo file = new FileInfo("WordBanList.txt");
                    /*
                     * - file exist verification
                     */
                    if (file.Exists )
                    {
                        List<string> lines = File.ReadAllLines("WordBanList.txt").ToList();

                        foreach (var line in lines)
                        {
                            wordBanList.AddWord(line);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case DataStorageArea.SERVER:                   

                    var result = this._serverReqwest.LoadData(wordBanList.GetType()).GetData();

                    wordBanList.AddWord(Encoding.Default.GetString(result));

                    return true;
            }
            return false;
        }
    }
}