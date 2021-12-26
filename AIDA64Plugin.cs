using FanControl.Plugins;
using System.Management;

namespace FanControl.AIDA64
{
    public class AIDA64Plugin : IPlugin
    {
        public string Name => "AIDA64";

        public void Initialize() { }

        public void Close() { }

        public void Load(IPluginSensorsContainer _container)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(new ManagementScope(@"\\.\root\WMI"), new ObjectQuery("SELECT * FROM AIDA64_SensorValues"));
            foreach (var obj in searcher.Get())
            {
                AddSensorToContainer(_container, obj);
            }
        }

        private static void AddSensorToContainer(IPluginSensorsContainer _container, ManagementBaseObject wmiObject)
        {
            var pluginSensor = new AIDA64PluginSensor(wmiObject);

            if (wmiObject["Type"].ToString() == "T")
                _container.TempSensors.Add(pluginSensor);
        }
    }

    public class AIDA64PluginSensor : IPluginSensor
    {
        private string _id;

        public AIDA64PluginSensor(ManagementBaseObject wmiObject)
        {
            _id = wmiObject["ID"].ToString();
        }

        public string Id => ReadObject()["ID"].ToString();

        public string Name => ReadObject()["Label"].ToString();

        public float? Value { get; private set; }

        public void Update() => Value = float.Parse(ReadObject()["Value"].ToString());

        private ManagementBaseObject ReadObject()
        {
            ManagementObject obj = new ManagementObject($@"\\.\root\WMI:AIDA64_SensorValues.ID='{_id}'");
            obj.Get();
            return obj;
        }
    }
}