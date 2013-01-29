using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace R12306
{
    [Serializable]
    class Config
    {
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private string _trainRe;

        public string TrainRe
        {
            get { return _trainRe; }
            set { _trainRe = value; }
        }

        private List<string> _selectPassengers = new List<string>();

        public List<string> SelectPassengers
        {
            get
            {
                if (_selectPassengers == null)
                {
                    _selectPassengers = new List<string>();
                }
                return _selectPassengers;
            }
        }

        private string _fromStationName;

        public string FromStationName
        {
            get { return _fromStationName; }
            set { _fromStationName = value; }
        }

        private string _toStationName;

        public string ToStationName
        {
            get { return _toStationName; }
            set { _toStationName = value; }
        }

        private int _seatIndex = 0;

        public int SeatIndex
        {
            get { return _seatIndex; }
            set { _seatIndex = value; }
        }
        

        [NonSerialized]
        private static Config _config = null;

        [NonSerialized]
        private static DateTime readFileTime;

        private static string ConfigFilePath
        {
            get
            {
                return Path.Combine(System.Windows.Forms.Application.StartupPath, Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath) + "_Config.bin");
            }

        }
        private Config()
        {
 
        }
        public static Config getConfig()
        {
            if (_config == null)
            {
                _config = LoadInfo();
                if (_config == null)
                    _config = new Config();
            }
            if (ConfigFileHasChangeed())
            {
                _config = LoadInfo();
            }
            return _config;
        }

        public static void SaveInfo()
        {
            IFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(ConfigFilePath, FileMode.Create);
            formatter.Serialize(stream, _config);
            stream.Close();
            readFileTime = File.GetLastWriteTime(ConfigFilePath);
        }

        private static Config LoadInfo()
        {

            if (File.Exists(ConfigFilePath))
            {
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    FileStream stream = new FileStream(ConfigFilePath, FileMode.Open);
                    Config newInf = (Config)formatter.Deserialize(stream);
                    stream.Close();
                    readFileTime = File.GetLastWriteTime(ConfigFilePath);
                    return newInf;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        private static bool ConfigFileHasChangeed()
        {
            if (File.Exists(ConfigFilePath))
            {
                DateTime lastwrite = File.GetLastWriteTime(ConfigFilePath);
                if (lastwrite > readFileTime)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
