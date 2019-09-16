using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RedScarf.EasyCSV.Demo
{
    public class UserDataManager : MonoBehaviour
    {
        static UserDataManager _instance = null;

        [SerializeField]
        private TextAsset file;
        private CsvTable table;

        private List<UserAccount> accounts = new List<UserAccount>();

        private void Awake() 
        {
            if (_instance != null && _instance != this)
            {
                Destroy(_instance.gameObject);
                _instance = this;
            }
            else
            {
                _instance = this;
            }

            DontDestroyOnLoad(this.gameObject);
        }

        public static UserDataManager GetInstance()
        {
            return _instance;
        }

        void Start()
        {
            CsvHelper.Init();

            table = CsvHelper.Create(file.name, file.text);
            //table.Read(0,0);
            //table.Write(0,0,"newValue");
        }



    }
}