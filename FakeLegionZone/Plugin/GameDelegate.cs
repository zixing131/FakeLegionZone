using FakeLegionZone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeLegionZone.Plugin
{
    public delegate void GameCleanMemoryCompletedDelegate(object sender, GameCleanMemoryCompletedReceivedData clearMemory);
    public delegate void GameHardwareInfoChangedDelegate(object sender, GameHardwareInfoChangedReceivedData hardwareInfo);
    public delegate void GameModeChangedDelegate(object sender, GameMode newMode, ChangeModeSenderType senderType); 
    public delegate void GameBatteryModeChangedDelegate(object sender, GameBatteryModeChangedReceivedData batteryMode);
    public delegate void GameExternalDeviceChangedDelegate(object sender, GameExternalDeviceChangedReceivedData externalDeviceInfo);

    public delegate void GameACPDBatteryModeChangedDelegate(object sender, GameACPDModeChangedReceivedData acpdMode);

    public delegate void GameInsideHookStartDelegate(MessageEventArgs<LZ_MSG_HOOK_START> messageEventArgs);
    public delegate void GameInsideInfobarShowedDelegate(MessageEventArgs messageEventArgs);

    public delegate void GameInsideInfobarHidedDelegate(MessageEventArgs messageEventArgs);

    public delegate void GameInsideChangeGameModeDelegate(MessageEventArgs<LZ_MSG_CHANGE_MODE> messageEventArgs);

    public delegate void GameInsideCleanMemoryDelegate(MessageEventArgs messageEventArgs);
    public delegate void GameInsideImageSnapedDelegate(MessageEventArgs<LZ_MSG_IMAGE_SNAPED> messageEventArgs);

    public delegate void GameInsideReportDelegate(MessageEventArgs<LZ_MSG_REPORT> messageEventArgs);
    public delegate void FromSelfMessageDelegate(MessageEventArgs<LZ_MSG_SELF_RUN_LZMAIN> messageEventArgs);
    public delegate void ExitMessageDelegate();
}
