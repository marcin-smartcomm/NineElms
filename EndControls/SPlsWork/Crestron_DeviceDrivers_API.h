namespace Crestron.DeviceDrivers.API.Capabilities.Events;
        // class declarations
         class CollectionChangedAction;
    static class CollectionChangedAction // enum
    {
        static SIGNED_LONG_INTEGER Added;
        static SIGNED_LONG_INTEGER Removed;
        static SIGNED_LONG_INTEGER Replaced;
        static SIGNED_LONG_INTEGER Reset;
    };

namespace Crestron.DeviceDrivers.API.Capabilities.Errors;
        // class declarations
         class DeviceErrorCodeSeverity;
         class DeviceErrorCode;
    static class DeviceErrorCodeSeverity // enum
    {
        static SIGNED_LONG_INTEGER Unknown;
        static SIGNED_LONG_INTEGER Error;
        static SIGNED_LONG_INTEGER Warning;
        static SIGNED_LONG_INTEGER Info;
    };

namespace Crestron.DeviceDrivers.API.Capabilities.PowerManagement;
        // class declarations
         class PoweredDeviceTraits;
         class RebootCapabilityFeatures;
         class RebootStatus;
    static class PoweredDeviceTraits // enum
    {
        static SIGNED_LONG_INTEGER Default;
        static SIGNED_LONG_INTEGER NotPluggable;
    };

    static class RebootCapabilityFeatures // enum
    {
        static SIGNED_LONG_INTEGER Default;
        static SIGNED_LONG_INTEGER RebootFeedback;
    };

    static class RebootStatus // enum
    {
        static SIGNED_LONG_INTEGER Unknown;
        static SIGNED_LONG_INTEGER Idle;
        static SIGNED_LONG_INTEGER Rebooting;
    };

namespace Crestron.DeviceDrivers.API.DataStructures.Units;
        // class declarations
         class TemperatureUnit;
         class EnumExtensions;
         class Temperature;
    static class TemperatureUnit // enum
    {
        static SIGNED_LONG_INTEGER Unknown;
        static SIGNED_LONG_INTEGER Fahrenheit;
        static SIGNED_LONG_INTEGER Celsius;
        static SIGNED_LONG_INTEGER Kelvin;
    };

    static class EnumExtensions 
    {
        // class delegates

        // class events

        // class functions
        static STRING_FUNCTION ToUnitsAbbreviation ( TemperatureUnit units );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

namespace Crestron.DeviceDrivers.API;
        // class declarations
         class LogEntryLevel;
    static class LogEntryLevel // enum
    {
        static SIGNED_LONG_INTEGER Off;
        static SIGNED_LONG_INTEGER Error;
        static SIGNED_LONG_INTEGER Warning;
        static SIGNED_LONG_INTEGER Info;
        static SIGNED_LONG_INTEGER Debug;
        static SIGNED_LONG_INTEGER Trace;
    };

namespace Crestron.DeviceDrivers.API.Collections;
        // class declarations
         class ImmutableArray;
    static class ImmutableArray 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

namespace Crestron.DeviceDrivers.API.Capabilities.Communications;
        // class declarations

