using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeLegionZone.Model
{  
    public class GPU : INotifyPropertyChanged
    {
        private int _Clock;
        public int Clock 
        {
            get
            {
                return _Clock;
            }
            set
            {    
                _Clock = value;
                Change("Clock");
            }
        }

        private int _Usage;
        public int Usage
        {
            get
            {
                return _Usage;
            }
            set
            {
                _Usage = value;
                Change("Usage");
            }
        }

        private int _Temperature;
        public int Temperature
        {
            get
            {
                return _Temperature;
            }
            set
            {
                _Temperature = value;
                Change("Temperature");
            }
        }
        private int _Power;
        public int Power
        {
            get
            {
                return _Power;
            }
            set
            {
                _Power = value;
                Change("Power");
            }
        }
        private int _FanSpeed;
        public int FanSpeed
        {
            get
            {
                return _FanSpeed;
            }
            set
            {
                _FanSpeed = value;
                Change("FanSpeed");
            }
        }
        private int _Mem_Usage;
        public int Mem_Usage
        {
            get
            {
                return _Mem_Usage;
            }
            set
            {
                _Mem_Usage = value;
                Change("Mem_Usage");
            }
        } 

        public event PropertyChangedEventHandler PropertyChanged;

        public void Change(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            MainWindow.SaveData();
        }
    }

    public class CPU : INotifyPropertyChanged
    {
        private int _Clock;
        public int Clock
        {
            get
            {
                return _Clock;
            }
            set
            {
                _Clock = value;
                Change("Clock");
            }
        }

        private int _Usage;
        public int Usage
        {
            get
            {
                return _Usage;
            }
            set
            {
                _Usage = value;
                Change("Usage");
            }
        }

        private int _Temperature;
        public int Temperature
        {
            get
            {
                return _Temperature;
            }
            set
            {
                _Temperature = value;
                Change("Temperature");
            }
        }
        private int _Power;
        public int Power
        {
            get
            {
                return _Power;
            }
            set
            {
                _Power = value;
                Change("Power");
            }
        }
        private int _FanSpeed;
        public int FanSpeed
        {
            get
            {
                return _FanSpeed;
            }
            set
            {
                _FanSpeed = value;
                Change("FanSpeed");
            }
        } 

        public event PropertyChangedEventHandler PropertyChanged;

        public void Change(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            MainWindow.SaveData();
        }

    }

    public class MEM: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Change(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            MainWindow.SaveData();
        }

        private int _Clock;
        public int Clock
        {
            get
            {
                return _Clock;
            }
            set
            {
                _Clock = value;
                Change("Clock");
            }
        }

        private int _Usage;
        public int Usage
        {
            get
            {
                return _Usage;
            }
            set
            {
                _Usage = value;
                Change("Usage");
            }
        }

    }

    public class PerformMointorData: INotifyPropertyChanged
    {

        private int _FPS;
        public int FPS
        {
            get
            {
                return _FPS;
            }
            set
            {
                _FPS = value;
                Change("FPS");
            }
        } 

        private GPU _GPU;
        public GPU GPU
        {
            get => _GPU;
            set
            {
                _GPU = value;
                Change("GPU");
            }
        } 

        private CPU _CPU;
        public CPU CPU
        {
            get => _CPU;
            set
            {
                _CPU = value;
                Change("CPU");
            }
        }

        private MEM _MEM;
        public MEM MEM 
        {
            get=> _MEM; 
            set {
                _MEM = value;
                Change("MEM");
            } 
        }

        private int _Orientation;
        public int Orientation
        {
            get
            {
                return _Orientation;
            }
            set
            {
                _Orientation = value;
                Change("Orientation");
            }
        }

        private int _Location;
        public int Location
        {
            get
            {
                return _Location;
            }
            set
            {
                _Location = value;
                Change("Location");
            }
        }

        private int _FontSize;
        public int FontSize
        {
            get
            {
                return _FontSize;
            }
            set
            {
                _FontSize = value;
                Change("FontSize");
            }
        } 

        public event PropertyChangedEventHandler PropertyChanged;

        public void Change(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

            MainWindow.SaveData(); 
        } 
    }  
}
