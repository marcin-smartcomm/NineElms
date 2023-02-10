using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using Crestron;
using Crestron.Logos.SplusLibrary;
using Crestron.Logos.SplusObjects;
using Crestron.SimplSharp;

namespace UserModule_SYMETRIX_VOLUME_PROCESSOR_V3_0
{
    public class UserModuleClass_SYMETRIX_VOLUME_PROCESSOR_V3_0 : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        
        
        Crestron.Logos.SplusObjects.DigitalInput VOL_UP;
        Crestron.Logos.SplusObjects.DigitalInput VOL_DOWN;
        Crestron.Logos.SplusObjects.AnalogInput SLIDER_IN;
        Crestron.Logos.SplusObjects.AnalogInput DIRECT_IN;
        Crestron.Logos.SplusObjects.StringInput FROM_COMMAND_PROCESSOR;
        Crestron.Logos.SplusObjects.DigitalOutput OUT_OF_RANGE;
        Crestron.Logos.SplusObjects.DigitalOutput USER_RANGE_FAULT;
        Crestron.Logos.SplusObjects.AnalogOutput BAR_OUT;
        Crestron.Logos.SplusObjects.StringOutput LEVEL_DB;
        Crestron.Logos.SplusObjects.StringOutput TO_COMMAND_PROCESSOR;
        UShortParameter HARDWARE_RANGE;
        ShortParameter USER_MIN;
        ShortParameter USER_MAX;
        UShortParameter INCREMENT_DB;
        StringParameter CONTROL_ID;
        ushort ICONTROLVALUE = 0;
        ushort IRESPONSEVALUE = 0;
        ushort IHARDWARESPANDB = 0;
        ushort IUSERSPANDB = 0;
        ushort IUSERSPAN = 0;
        ushort IINCREMENT = 0;
        short IUSERMINDB = 0;
        short IUSERMAXDB = 0;
        ushort IUSERMIN = 0;
        ushort IUSERMAX = 0;
        ushort IDBWHOLE = 0;
        ushort IDBDEC = 0;
        ushort ISLIDERCONVERT = 0;
        ushort IUPDATERECEIVED = 0;
        ushort IDIRECTCONVERT = 0;
        ushort IBARBASE = 0;
        short HARDWARE_MIN = 0;
        short HARDWARE_MAX = 0;
        CrestronString ITEMP;
        uint IDBCALCLONG = 0;
        private ushort FGETROUNDED (  SplusExecutionContext __context__, ushort V1 , ushort V2 , ushort V3 ) 
            { 
            uint LONGTEMP = 0;
            
            ushort INTTEMP = 0;
            
            
            __context__.SourceCodeLine = 110;
            INTTEMP = (ushort) ( Functions.MulDiv( (ushort)( V1 ) , (ushort)( V2 ) , (ushort)( V3 ) ) ) ; 
            __context__.SourceCodeLine = 111;
            LONGTEMP = (uint) ( (V1 * V2) ) ; 
            __context__.SourceCodeLine = 112;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Mod( LONGTEMP , V3 ) >= (V3 / 2) ))  ) ) 
                { 
                __context__.SourceCodeLine = 114;
                INTTEMP = (ushort) ( (INTTEMP + 1) ) ; 
                } 
            
            __context__.SourceCodeLine = 116;
            return (ushort)( INTTEMP) ; 
            
            }
            
        object VOL_UP_OnPush_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 125;
                while ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( VOL_UP  .Value ) && Functions.TestForTrue ( Functions.BoolToInt ( IRESPONSEVALUE < IUSERMAX ) )) ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 127;
                    if ( Functions.TestForTrue  ( ( IUPDATERECEIVED)  ) ) 
                        { 
                        __context__.SourceCodeLine = 129;
                        IUPDATERECEIVED = (ushort) ( 0 ) ; 
                        __context__.SourceCodeLine = 130;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( IRESPONSEVALUE < IUSERMIN ))  ) ) 
                            { 
                            __context__.SourceCodeLine = 132;
                            MakeString ( TO_COMMAND_PROCESSOR , "CS {0} {1:d}\r", CONTROL_ID , (ushort)IUSERMIN) ; 
                            } 
                        
                        else 
                            {
                            __context__.SourceCodeLine = 134;
                            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( IRESPONSEVALUE < (IUSERMAX - IINCREMENT) ))  ) ) 
                                { 
                                __context__.SourceCodeLine = 136;
                                MakeString ( TO_COMMAND_PROCESSOR , "CS {0} {1:d}\r", CONTROL_ID , (ushort)(IRESPONSEVALUE + IINCREMENT)) ; 
                                } 
                            
                            else 
                                { 
                                __context__.SourceCodeLine = 140;
                                MakeString ( TO_COMMAND_PROCESSOR , "CS {0} {1:d}\r", CONTROL_ID , (ushort)IUSERMAX) ; 
                                } 
                            
                            }
                        
                        __context__.SourceCodeLine = 142;
                        Functions.Delay (  (int) ( 25 ) ) ; 
                        } 
                    
                    __context__.SourceCodeLine = 125;
                    } 
                
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object VOL_DOWN_OnPush_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 149;
            while ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( VOL_DOWN  .Value ) && Functions.TestForTrue ( Functions.BoolToInt ( IRESPONSEVALUE > IUSERMIN ) )) ))  ) ) 
                { 
                __context__.SourceCodeLine = 151;
                if ( Functions.TestForTrue  ( ( IUPDATERECEIVED)  ) ) 
                    { 
                    __context__.SourceCodeLine = 153;
                    IUPDATERECEIVED = (ushort) ( 0 ) ; 
                    __context__.SourceCodeLine = 154;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( IRESPONSEVALUE > IUSERMAX ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 156;
                        MakeString ( TO_COMMAND_PROCESSOR , "CS {0} {1:d}\r", CONTROL_ID , (ushort)IUSERMAX) ; 
                        } 
                    
                    else 
                        {
                        __context__.SourceCodeLine = 158;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (IRESPONSEVALUE - IINCREMENT) > IUSERMIN ))  ) ) 
                            { 
                            __context__.SourceCodeLine = 160;
                            MakeString ( TO_COMMAND_PROCESSOR , "CS {0} {1:d}\r", CONTROL_ID , (ushort)(IRESPONSEVALUE - IINCREMENT)) ; 
                            } 
                        
                        else 
                            { 
                            __context__.SourceCodeLine = 164;
                            MakeString ( TO_COMMAND_PROCESSOR , "CS {0} {1:d}\r", CONTROL_ID , (ushort)IUSERMIN) ; 
                            } 
                        
                        }
                    
                    __context__.SourceCodeLine = 166;
                    Functions.Delay (  (int) ( 25 ) ) ; 
                    } 
                
                __context__.SourceCodeLine = 149;
                } 
            
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    
object SLIDER_IN_OnChange_2 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 173;
        ISLIDERCONVERT = (ushort) ( (Functions.MulDiv( (ushort)( SLIDER_IN  .UshortValue ) , (ushort)( IUSERSPAN ) , (ushort)( 65535 ) ) + IUSERMIN) ) ; 
        __context__.SourceCodeLine = 174;
        MakeString ( TO_COMMAND_PROCESSOR , "CS {0} {1:d}\r", CONTROL_ID , (ushort)ISLIDERCONVERT) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object DIRECT_IN_OnChange_3 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        short DIRECT_IN_SIGNED = 0;
        
        
        __context__.SourceCodeLine = 181;
        DIRECT_IN_SIGNED = (short) ( DIRECT_IN  .ShortValue ) ; 
        __context__.SourceCodeLine = 182;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( DIRECT_IN_SIGNED >= IUSERMINDB ) ) && Functions.TestForTrue ( Functions.BoolToInt ( DIRECT_IN_SIGNED <= IUSERMAXDB ) )) ))  ) ) 
            { 
            __context__.SourceCodeLine = 184;
            IDIRECTCONVERT = (ushort) ( FGETROUNDED( __context__ , (ushort)( (DIRECT_IN  .UshortValue - HARDWARE_MIN) ) , (ushort)( 65535 ) , (ushort)( IHARDWARESPANDB ) ) ) ; 
            __context__.SourceCodeLine = 185;
            MakeString ( TO_COMMAND_PROCESSOR , "CS {0} {1:d}\r", CONTROL_ID , (ushort)IDIRECTCONVERT) ; 
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object FROM_COMMAND_PROCESSOR_OnChange_4 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        uint LIBARCALC = 0;
        
        
        __context__.SourceCodeLine = 192;
        if ( Functions.TestForTrue  ( ( Functions.Find( "Send Info\r" , FROM_COMMAND_PROCESSOR ))  ) ) 
            { 
            __context__.SourceCodeLine = 194;
            MakeString ( TO_COMMAND_PROCESSOR , "{0:d} Send Info 0 {1}\r", (ushort)Functions.Atoi( FROM_COMMAND_PROCESSOR ), CONTROL_ID ) ; 
            } 
        
        else 
            {
            __context__.SourceCodeLine = 196;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Atoi( CONTROL_ID  ) == Functions.Atoi( FROM_COMMAND_PROCESSOR )))  ) ) 
                { 
                __context__.SourceCodeLine = 198;
                ITEMP  .UpdateValue ( Functions.Remove ( "=" , FROM_COMMAND_PROCESSOR )  ) ; 
                __context__.SourceCodeLine = 199;
                IRESPONSEVALUE = (ushort) ( Functions.Atoi( FROM_COMMAND_PROCESSOR ) ) ; 
                __context__.SourceCodeLine = 200;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( IRESPONSEVALUE < IUSERMIN ) ) || Functions.TestForTrue ( Functions.BoolToInt ( IRESPONSEVALUE > IUSERMAX ) )) ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 202;
                    OUT_OF_RANGE  .Value = (ushort) ( 1 ) ; 
                    __context__.SourceCodeLine = 203;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( IRESPONSEVALUE < IUSERMIN ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 205;
                        BAR_OUT  .Value = (ushort) ( 0 ) ; 
                        } 
                    
                    else 
                        { 
                        __context__.SourceCodeLine = 209;
                        BAR_OUT  .Value = (ushort) ( 65535 ) ; 
                        } 
                    
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 214;
                    OUT_OF_RANGE  .Value = (ushort) ( 0 ) ; 
                    __context__.SourceCodeLine = 215;
                    LIBARCALC = (uint) ( ((IRESPONSEVALUE - IUSERMIN) * 65535) ) ; 
                    __context__.SourceCodeLine = 216;
                    BAR_OUT  .Value = (ushort) ( (LIBARCALC / IUSERSPAN) ) ; 
                    } 
                
                __context__.SourceCodeLine = 218;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (IRESPONSEVALUE == 0))  ) ) 
                    { 
                    __context__.SourceCodeLine = 220;
                    LEVEL_DB  .UpdateValue ( "Off"  ) ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 224;
                    IDBWHOLE = (ushort) ( (FGETROUNDED( __context__ , (ushort)( IRESPONSEVALUE ) , (ushort)( IHARDWARESPANDB ) , (ushort)( 65535 ) ) + HARDWARE_MIN) ) ; 
                    __context__.SourceCodeLine = 225;
                    MakeString ( LEVEL_DB , "{0:d} dB", (short)IDBWHOLE) ; 
                    } 
                
                __context__.SourceCodeLine = 227;
                IUPDATERECEIVED = (ushort) ( 1 ) ; 
                } 
            
            }
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

public override object FunctionMain (  object __obj__ ) 
    { 
    try
    {
        SplusExecutionContext __context__ = SplusFunctionMainStartCode();
        
        __context__.SourceCodeLine = 238;
        WaitForInitializationComplete ( ) ; 
        __context__.SourceCodeLine = 239;
        IUPDATERECEIVED = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 240;
        
            {
            int __SPLS_TMPVAR__SWTCH_1__ = ((int)HARDWARE_RANGE  .Value);
            
                { 
                if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 1) ) ) ) 
                    { 
                    __context__.SourceCodeLine = 244;
                    HARDWARE_MIN = (short) ( Functions.ToSignedInteger( -( 72 ) ) ) ; 
                    __context__.SourceCodeLine = 245;
                    HARDWARE_MAX = (short) ( 12 ) ; 
                    } 
                
                else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 2) ) ) ) 
                    { 
                    __context__.SourceCodeLine = 249;
                    HARDWARE_MIN = (short) ( Functions.ToSignedInteger( -( 24 ) ) ) ; 
                    __context__.SourceCodeLine = 250;
                    HARDWARE_MAX = (short) ( 24 ) ; 
                    } 
                
                else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 3) ) ) ) 
                    { 
                    __context__.SourceCodeLine = 254;
                    HARDWARE_MIN = (short) ( Functions.ToSignedInteger( -( 40 ) ) ) ; 
                    __context__.SourceCodeLine = 255;
                    HARDWARE_MAX = (short) ( 20 ) ; 
                    } 
                
                else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 4) ) ) ) 
                    { 
                    __context__.SourceCodeLine = 259;
                    HARDWARE_MIN = (short) ( Functions.ToSignedInteger( -( 40 ) ) ) ; 
                    __context__.SourceCodeLine = 260;
                    HARDWARE_MAX = (short) ( 0 ) ; 
                    } 
                
                else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 5) ) ) ) 
                    { 
                    __context__.SourceCodeLine = 264;
                    HARDWARE_MIN = (short) ( Functions.ToSignedInteger( -( 20 ) ) ) ; 
                    __context__.SourceCodeLine = 265;
                    HARDWARE_MAX = (short) ( 0 ) ; 
                    } 
                
                else if  ( Functions.TestForTrue  (  ( __SPLS_TMPVAR__SWTCH_1__ == ( 6) ) ) ) 
                    { 
                    __context__.SourceCodeLine = 269;
                    HARDWARE_MIN = (short) ( 0 ) ; 
                    __context__.SourceCodeLine = 270;
                    HARDWARE_MAX = (short) ( 20 ) ; 
                    } 
                
                } 
                
            }
            
        
        __context__.SourceCodeLine = 274;
        IHARDWARESPANDB = (ushort) ( (HARDWARE_MAX - HARDWARE_MIN) ) ; 
        __context__.SourceCodeLine = 276;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( USER_MIN  .Value >= HARDWARE_MIN ) ) && Functions.TestForTrue ( Functions.BoolToInt ( USER_MIN  .Value <= HARDWARE_MAX ) )) ))  ) ) 
            { 
            __context__.SourceCodeLine = 278;
            IUSERMINDB = (short) ( USER_MIN  .Value ) ; 
            } 
        
        else 
            { 
            __context__.SourceCodeLine = 282;
            IUSERMINDB = (short) ( HARDWARE_MIN ) ; 
            __context__.SourceCodeLine = 283;
            USER_RANGE_FAULT  .Value = (ushort) ( 1 ) ; 
            } 
        
        __context__.SourceCodeLine = 286;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( USER_MAX  .Value >= HARDWARE_MIN ) ) && Functions.TestForTrue ( Functions.BoolToInt ( USER_MAX  .Value <= HARDWARE_MAX ) )) ))  ) ) 
            { 
            __context__.SourceCodeLine = 288;
            IUSERMAXDB = (short) ( USER_MAX  .Value ) ; 
            } 
        
        else 
            { 
            __context__.SourceCodeLine = 292;
            IUSERMAXDB = (short) ( HARDWARE_MAX ) ; 
            __context__.SourceCodeLine = 293;
            USER_RANGE_FAULT  .Value = (ushort) ( 1 ) ; 
            } 
        
        __context__.SourceCodeLine = 295;
        if ( Functions.TestForTrue  ( ( 0)  ) ) 
            {
            __context__.SourceCodeLine = 296;
            Trace( "{0} iHardwareSpandB={1:d}, iUserMindB={2:d}, iUserMaxdB={3:d}\r\n", GetSymbolInstanceName ( ) , (ushort)IHARDWARESPANDB, (short)IUSERMINDB, (short)IUSERMAXDB) ; 
            }
        
        __context__.SourceCodeLine = 298;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( IUSERMAXDB > IUSERMINDB ))  ) ) 
            { 
            __context__.SourceCodeLine = 300;
            IUSERSPANDB = (ushort) ( (IUSERMAXDB - IUSERMINDB) ) ; 
            } 
        
        else 
            { 
            __context__.SourceCodeLine = 304;
            IUSERMAXDB = (short) ( HARDWARE_MAX ) ; 
            __context__.SourceCodeLine = 305;
            IUSERMINDB = (short) ( HARDWARE_MIN ) ; 
            __context__.SourceCodeLine = 306;
            IUSERSPANDB = (ushort) ( IHARDWARESPANDB ) ; 
            __context__.SourceCodeLine = 307;
            USER_RANGE_FAULT  .Value = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 308;
            Trace( "{0} User Max is Less Than or Equal To User Min - Using Hardware Limits\r\n", GetSymbolInstanceName ( ) ) ; 
            } 
        
        __context__.SourceCodeLine = 311;
        IUSERSPAN = (ushort) ( FGETROUNDED( __context__ , (ushort)( IUSERSPANDB ) , (ushort)( 65535 ) , (ushort)( IHARDWARESPANDB ) ) ) ; 
        __context__.SourceCodeLine = 313;
        IINCREMENT = (ushort) ( FGETROUNDED( __context__ , (ushort)( INCREMENT_DB  .Value ) , (ushort)( 65535 ) , (ushort)( IHARDWARESPANDB ) ) ) ; 
        __context__.SourceCodeLine = 315;
        IUSERMIN = (ushort) ( FGETROUNDED( __context__ , (ushort)( (IUSERMINDB - HARDWARE_MIN) ) , (ushort)( 65535 ) , (ushort)( IHARDWARESPANDB ) ) ) ; 
        __context__.SourceCodeLine = 317;
        IUSERMAX = (ushort) ( FGETROUNDED( __context__ , (ushort)( (IUSERMAXDB - HARDWARE_MIN) ) , (ushort)( 65535 ) , (ushort)( IHARDWARESPANDB ) ) ) ; 
        __context__.SourceCodeLine = 318;
        if ( Functions.TestForTrue  ( ( 0)  ) ) 
            {
            __context__.SourceCodeLine = 319;
            Trace( "{0} iUserSpandB={1:d}, iUserMin={2:d}, iUserMax={3:d}, iUserSpan={4:d}, iIncrement={5:d}\r\n", GetSymbolInstanceName ( ) , (ushort)IUSERSPANDB, (ushort)IUSERMIN, (ushort)IUSERMAX, (ushort)IUSERSPAN, (ushort)IINCREMENT) ; 
            }
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    return __obj__;
    }
    

public override void LogosSplusInitialize()
{
    _SplusNVRAM = new SplusNVRAM( this );
    ITEMP  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 100, this );
    
    VOL_UP = new Crestron.Logos.SplusObjects.DigitalInput( VOL_UP__DigitalInput__, this );
    m_DigitalInputList.Add( VOL_UP__DigitalInput__, VOL_UP );
    
    VOL_DOWN = new Crestron.Logos.SplusObjects.DigitalInput( VOL_DOWN__DigitalInput__, this );
    m_DigitalInputList.Add( VOL_DOWN__DigitalInput__, VOL_DOWN );
    
    OUT_OF_RANGE = new Crestron.Logos.SplusObjects.DigitalOutput( OUT_OF_RANGE__DigitalOutput__, this );
    m_DigitalOutputList.Add( OUT_OF_RANGE__DigitalOutput__, OUT_OF_RANGE );
    
    USER_RANGE_FAULT = new Crestron.Logos.SplusObjects.DigitalOutput( USER_RANGE_FAULT__DigitalOutput__, this );
    m_DigitalOutputList.Add( USER_RANGE_FAULT__DigitalOutput__, USER_RANGE_FAULT );
    
    SLIDER_IN = new Crestron.Logos.SplusObjects.AnalogInput( SLIDER_IN__AnalogSerialInput__, this );
    m_AnalogInputList.Add( SLIDER_IN__AnalogSerialInput__, SLIDER_IN );
    
    DIRECT_IN = new Crestron.Logos.SplusObjects.AnalogInput( DIRECT_IN__AnalogSerialInput__, this );
    m_AnalogInputList.Add( DIRECT_IN__AnalogSerialInput__, DIRECT_IN );
    
    BAR_OUT = new Crestron.Logos.SplusObjects.AnalogOutput( BAR_OUT__AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( BAR_OUT__AnalogSerialOutput__, BAR_OUT );
    
    FROM_COMMAND_PROCESSOR = new Crestron.Logos.SplusObjects.StringInput( FROM_COMMAND_PROCESSOR__AnalogSerialInput__, 100, this );
    m_StringInputList.Add( FROM_COMMAND_PROCESSOR__AnalogSerialInput__, FROM_COMMAND_PROCESSOR );
    
    LEVEL_DB = new Crestron.Logos.SplusObjects.StringOutput( LEVEL_DB__AnalogSerialOutput__, this );
    m_StringOutputList.Add( LEVEL_DB__AnalogSerialOutput__, LEVEL_DB );
    
    TO_COMMAND_PROCESSOR = new Crestron.Logos.SplusObjects.StringOutput( TO_COMMAND_PROCESSOR__AnalogSerialOutput__, this );
    m_StringOutputList.Add( TO_COMMAND_PROCESSOR__AnalogSerialOutput__, TO_COMMAND_PROCESSOR );
    
    HARDWARE_RANGE = new UShortParameter( HARDWARE_RANGE__Parameter__, this );
    m_ParameterList.Add( HARDWARE_RANGE__Parameter__, HARDWARE_RANGE );
    
    INCREMENT_DB = new UShortParameter( INCREMENT_DB__Parameter__, this );
    m_ParameterList.Add( INCREMENT_DB__Parameter__, INCREMENT_DB );
    
    USER_MIN = new ShortParameter( USER_MIN__Parameter__, this );
    m_ParameterList.Add( USER_MIN__Parameter__, USER_MIN );
    
    USER_MAX = new ShortParameter( USER_MAX__Parameter__, this );
    m_ParameterList.Add( USER_MAX__Parameter__, USER_MAX );
    
    CONTROL_ID = new StringParameter( CONTROL_ID__Parameter__, this );
    m_ParameterList.Add( CONTROL_ID__Parameter__, CONTROL_ID );
    
    
    VOL_UP.OnDigitalPush.Add( new InputChangeHandlerWrapper( VOL_UP_OnPush_0, false ) );
    VOL_DOWN.OnDigitalPush.Add( new InputChangeHandlerWrapper( VOL_DOWN_OnPush_1, false ) );
    SLIDER_IN.OnAnalogChange.Add( new InputChangeHandlerWrapper( SLIDER_IN_OnChange_2, false ) );
    DIRECT_IN.OnAnalogChange.Add( new InputChangeHandlerWrapper( DIRECT_IN_OnChange_3, false ) );
    FROM_COMMAND_PROCESSOR.OnSerialChange.Add( new InputChangeHandlerWrapper( FROM_COMMAND_PROCESSOR_OnChange_4, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_SYMETRIX_VOLUME_PROCESSOR_V3_0 ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint VOL_UP__DigitalInput__ = 0;
const uint VOL_DOWN__DigitalInput__ = 1;
const uint SLIDER_IN__AnalogSerialInput__ = 0;
const uint DIRECT_IN__AnalogSerialInput__ = 1;
const uint FROM_COMMAND_PROCESSOR__AnalogSerialInput__ = 2;
const uint OUT_OF_RANGE__DigitalOutput__ = 0;
const uint USER_RANGE_FAULT__DigitalOutput__ = 1;
const uint BAR_OUT__AnalogSerialOutput__ = 0;
const uint LEVEL_DB__AnalogSerialOutput__ = 1;
const uint TO_COMMAND_PROCESSOR__AnalogSerialOutput__ = 2;
const uint HARDWARE_RANGE__Parameter__ = 10;
const uint USER_MIN__Parameter__ = 11;
const uint USER_MAX__Parameter__ = 12;
const uint INCREMENT_DB__Parameter__ = 13;
const uint CONTROL_ID__Parameter__ = 14;

[SplusStructAttribute(-1, true, false)]
public class SplusNVRAM : SplusStructureBase
{

    public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
    
    
}

SplusNVRAM _SplusNVRAM = null;

public class __CEvent__ : CEvent
{
    public __CEvent__() {}
    public void Close() { base.Close(); }
    public int Reset() { return base.Reset() ? 1 : 0; }
    public int Set() { return base.Set() ? 1 : 0; }
    public int Wait( int timeOutInMs ) { return base.Wait( timeOutInMs ) ? 1 : 0; }
}
public class __CMutex__ : CMutex
{
    public __CMutex__() {}
    public void Close() { base.Close(); }
    public void ReleaseMutex() { base.ReleaseMutex(); }
    public int WaitForMutex() { return base.WaitForMutex() ? 1 : 0; }
}
 public int IsNull( object obj ){ return (obj == null) ? 1 : 0; }
}


}
