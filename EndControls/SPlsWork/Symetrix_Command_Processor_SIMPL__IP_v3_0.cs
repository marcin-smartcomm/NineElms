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

namespace UserModule_SYMETRIX_COMMAND_PROCESSOR_SIMPL__IP_V3_0
{
    public class UserModuleClass_SYMETRIX_COMMAND_PROCESSOR_SIMPL__IP_V3_0 : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        Crestron.Logos.SplusObjects.DigitalInput INITIALIZE;
        Crestron.Logos.SplusObjects.DigitalInput GET_NEXT_INFO;
        Crestron.Logos.SplusObjects.DigitalInput SEND_NEXT;
        Crestron.Logos.SplusObjects.DigitalInput STOP_INIT_AFTER_10_NO_REPLIES;
        Crestron.Logos.SplusObjects.DigitalInput CLIENT_CONNECT_FB;
        Crestron.Logos.SplusObjects.AnalogInput CLIENT_CONNECT_STATUS_FB;
        Crestron.Logos.SplusObjects.BufferInput FROM_DEVICE;
        Crestron.Logos.SplusObjects.BufferInput FROM_MODULES;
        Crestron.Logos.SplusObjects.DigitalOutput INITIALIZE_BUSY;
        Crestron.Logos.SplusObjects.DigitalOutput INFO_TIMED_OUT;
        Crestron.Logos.SplusObjects.DigitalOutput TIMED_OUT;
        Crestron.Logos.SplusObjects.DigitalOutput CLIENT_CONNECT;
        Crestron.Logos.SplusObjects.StringOutput TO_DEVICE;
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> TO_MODULES;
        ushort INEXTCOMMANDSTORE = 0;
        ushort INEXTCOMMANDSEND = 0;
        ushort ITEMPINSTANCE = 0;
        ushort ITEMPCONTROLID = 0;
        ushort ITEMPCONTROLID1 = 0;
        ushort ITEMPCONTROLID2 = 0;
        ushort BFLAG1 = 0;
        ushort BFLAG2 = 0;
        ushort BOKTOSEND = 0;
        ushort ITEMP = 0;
        ushort ISENDINFO = 0;
        ushort ITEMPVALUE = 0;
        ushort A = 0;
        ushort ILASTMODULECONNECTED = 0;
        ushort IRESENDCOUNT = 0;
        ushort [] ICONTROLID1;
        ushort [] ICONTROLID2;
        ushort [] ICONTROLTYPE;
        ushort [] IPHONEUNIT;
        ushort [] IPHONECARD;
        ushort [] IPHONELINE;
        ushort ITEMPUNIT = 0;
        ushort ITEMPRESOURCE = 0;
        ushort ITEMPENUM = 0;
        ushort ITEMPCARD = 0;
        ushort ITEMPCHANNEL = 0;
        ushort IPHONEPARSETYPE = 0;
        CrestronString [] SCOMMAND;
        CrestronString STEMPMODULES;
        CrestronString STEMPDEVICE;
        CrestronString STRASH;
        CrestronString STEMP;
        CrestronString SLASTSENT;
        CrestronString SPHONEDATA;
        ushort IINFONOANSWERCOUNT = 0;
        ushort BISHEARTBEATING = 0;
        ushort BISCOMMUNICATING = 0;
        ushort BPHONECONTROLINUSE = 0;
        ushort BINITIALIZATIONISCOMPLETE = 0;
        ushort IDEVICEMESSAGETIMEOUTCOUNT = 0;
        private void FTIMEOUT (  SplusExecutionContext __context__ ) 
            { 
            
            __context__.SourceCodeLine = 149;
            CreateWait ( "WTIMEOUT" , 200 , WTIMEOUT_Callback ) ;
            
            }
            
        public void WTIMEOUT_CallbackFn( object stateInfo )
        {
        
            try
            {
                Wait __LocalWait__ = (Wait)stateInfo;
                SplusExecutionContext __context__ = SplusThreadStartCode(__LocalWait__);
                __LocalWait__.RemoveFromList();
                
            
            __context__.SourceCodeLine = 151;
            TIMED_OUT  .Value = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 152;
            IDEVICEMESSAGETIMEOUTCOUNT = (ushort) ( (IDEVICEMESSAGETIMEOUTCOUNT + 1) ) ; 
            __context__.SourceCodeLine = 153;
            Trace( "Symnet command timed out - timeout count={0:d}\r\n", (ushort)IDEVICEMESSAGETIMEOUTCOUNT) ; 
            __context__.SourceCodeLine = 154;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( IDEVICEMESSAGETIMEOUTCOUNT > 2 ))  ) ) 
                { 
                __context__.SourceCodeLine = 156;
                BISCOMMUNICATING = (ushort) ( 0 ) ; 
                __context__.SourceCodeLine = 157;
                Trace( "Symnet timeout limit reached - Symnet device not communicating\r\n") ; 
                } 
            
            
        
        
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler(); }
            
        }
        
    private void FPROCESSMESSAGETODEVICE (  SplusExecutionContext __context__, CrestronString MESSAGE ) 
        { 
        
        __context__.SourceCodeLine = 164;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (BOKTOSEND == 1))  ) ) 
            { 
            __context__.SourceCodeLine = 166;
            BOKTOSEND = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 167;
            SLASTSENT  .UpdateValue ( Functions.Left ( MESSAGE ,  (int) ( (Functions.Length( MESSAGE ) - 1) ) )  ) ; 
            __context__.SourceCodeLine = 168;
            FTIMEOUT (  __context__  ) ; 
            __context__.SourceCodeLine = 169;
            TO_DEVICE  .UpdateValue ( "$e " + MESSAGE  ) ; 
            __context__.SourceCodeLine = 170;
            if ( Functions.TestForTrue  ( ( 0)  ) ) 
                {
                __context__.SourceCodeLine = 171;
                Trace( "To Device=$e {0} sLastSent={1}\r\n", MESSAGE , SLASTSENT ) ; 
                }
            
            } 
        
        else 
            { 
            __context__.SourceCodeLine = 175;
            SCOMMAND [ INEXTCOMMANDSTORE ]  .UpdateValue ( MESSAGE  ) ; 
            __context__.SourceCodeLine = 176;
            INEXTCOMMANDSTORE = (ushort) ( (INEXTCOMMANDSTORE + 1) ) ; 
            __context__.SourceCodeLine = 177;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( INEXTCOMMANDSTORE > 500 ))  ) ) 
                { 
                __context__.SourceCodeLine = 179;
                INEXTCOMMANDSTORE = (ushort) ( 1 ) ; 
                } 
            
            } 
        
        
        }
        
    private void FSENDHEARTBEAT (  SplusExecutionContext __context__ ) 
        { 
        
        __context__.SourceCodeLine = 186;
        if ( Functions.TestForTrue  ( ( BISHEARTBEATING)  ) ) 
            { 
            __context__.SourceCodeLine = 188;
            if ( Functions.TestForTrue  ( ( BOKTOSEND)  ) ) 
                { 
                __context__.SourceCodeLine = 190;
                FPROCESSMESSAGETODEVICE (  __context__ , "NOP\r") ; 
                } 
            
            __context__.SourceCodeLine = 193;
            CreateWait ( "HEARTBEAT" , 3000 , HEARTBEAT_Callback ) ;
            } 
        
        
        }
        
    public void HEARTBEAT_CallbackFn( object stateInfo )
    {
    
        try
        {
            Wait __LocalWait__ = (Wait)stateInfo;
            SplusExecutionContext __context__ = SplusThreadStartCode(__LocalWait__);
            __LocalWait__.RemoveFromList();
            
            
            __context__.SourceCodeLine = 195;
            if ( Functions.TestForTrue  ( ( BISHEARTBEATING)  ) ) 
                {
                __context__.SourceCodeLine = 196;
                FSENDHEARTBEAT (  __context__  ) ; 
                }
            
            
        
        
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler(); }
        
    }
    
private void FSTARTHEARTBEAT (  SplusExecutionContext __context__ ) 
    { 
    
    __context__.SourceCodeLine = 203;
    if ( Functions.TestForTrue  ( ( Functions.Not( BISHEARTBEATING ))  ) ) 
        { 
        __context__.SourceCodeLine = 205;
        BISHEARTBEATING = (ushort) ( 1 ) ; 
        __context__.SourceCodeLine = 207;
        FSENDHEARTBEAT (  __context__  ) ; 
        } 
    
    
    }
    
private void FSTOPHEARTBEAT (  SplusExecutionContext __context__ ) 
    { 
    
    __context__.SourceCodeLine = 213;
    BISHEARTBEATING = (ushort) ( 0 ) ; 
    __context__.SourceCodeLine = 215;
    CancelWait ( "HEARTBEAT" ) ; 
    
    }
    
private void FSETISCOMMUNICATING (  SplusExecutionContext __context__ ) 
    { 
    
    __context__.SourceCodeLine = 220;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.Not( BISCOMMUNICATING ) ) && Functions.TestForTrue ( BINITIALIZATIONISCOMPLETE )) ))  ) ) 
        { 
        __context__.SourceCodeLine = 222;
        BISCOMMUNICATING = (ushort) ( 1 ) ; 
        __context__.SourceCodeLine = 223;
        FPROCESSMESSAGETODEVICE (  __context__ , "PUR\r") ; 
        __context__.SourceCodeLine = 224;
        if ( Functions.TestForTrue  ( ( BPHONECONTROLINUSE)  ) ) 
            { 
            __context__.SourceCodeLine = 226;
            Functions.Delay (  (int) ( 100 ) ) ; 
            __context__.SourceCodeLine = 227;
            FPROCESSMESSAGETODEVICE (  __context__ , "PURSS\r") ; 
            } 
        
        __context__.SourceCodeLine = 229;
        Trace( "Symnet device is communicating\r\n") ; 
        __context__.SourceCodeLine = 230;
        FSTARTHEARTBEAT (  __context__  ) ; 
        } 
    
    
    }
    
private void FINFOTIMEOUT (  SplusExecutionContext __context__ ) 
    { 
    
    __context__.SourceCodeLine = 236;
    CreateWait ( "WINFOTIMEOUT" , 25 , WINFOTIMEOUT_Callback ) ;
    
    }
    
public void WINFOTIMEOUT_CallbackFn( object stateInfo )
{

    try
    {
        Wait __LocalWait__ = (Wait)stateInfo;
        SplusExecutionContext __context__ = SplusThreadStartCode(__LocalWait__);
        __LocalWait__.RemoveFromList();
        
            
            __context__.SourceCodeLine = 238;
            IINFONOANSWERCOUNT = (ushort) ( (IINFONOANSWERCOUNT + 1) ) ; 
            __context__.SourceCodeLine = 239;
            INFO_TIMED_OUT  .Value = (ushort) ( 1 ) ; 
            
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    
}

private void FSENDNEXT (  SplusExecutionContext __context__ ) 
    { 
    
    __context__.SourceCodeLine = 245;
    IRESENDCOUNT = (ushort) ( 0 ) ; 
    __context__.SourceCodeLine = 246;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Length( SCOMMAND[ INEXTCOMMANDSEND ] ) > 0 ))  ) ) 
        { 
        __context__.SourceCodeLine = 248;
        SLASTSENT  .UpdateValue ( Functions.Left ( SCOMMAND [ INEXTCOMMANDSEND ] ,  (int) ( (Functions.Length( SCOMMAND[ INEXTCOMMANDSEND ] ) - 1) ) )  ) ; 
        __context__.SourceCodeLine = 249;
        if ( Functions.TestForTrue  ( ( 0)  ) ) 
            {
            __context__.SourceCodeLine = 250;
            Trace( "To Device=$e {0} sLastSent={1}\r\n", SCOMMAND [ INEXTCOMMANDSEND ] , SLASTSENT ) ; 
            }
        
        __context__.SourceCodeLine = 251;
        BOKTOSEND = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 252;
        FTIMEOUT (  __context__  ) ; 
        __context__.SourceCodeLine = 253;
        TO_DEVICE  .UpdateValue ( "$e " + SCOMMAND [ INEXTCOMMANDSEND ]  ) ; 
        __context__.SourceCodeLine = 254;
        SCOMMAND [ INEXTCOMMANDSEND ]  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 255;
        INEXTCOMMANDSEND = (ushort) ( (INEXTCOMMANDSEND + 1) ) ; 
        __context__.SourceCodeLine = 256;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( INEXTCOMMANDSEND > 500 ))  ) ) 
            { 
            __context__.SourceCodeLine = 258;
            INEXTCOMMANDSEND = (ushort) ( 1 ) ; 
            } 
        
        } 
    
    else 
        { 
        __context__.SourceCodeLine = 263;
        BOKTOSEND = (ushort) ( 1 ) ; 
        } 
    
    
    }
    
object INITIALIZE_OnPush_0 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 273;
        INITIALIZE_BUSY  .Value = (ushort) ( 1 ) ; 
        __context__.SourceCodeLine = 274;
        BINITIALIZATIONISCOMPLETE = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 275;
        IINFONOANSWERCOUNT = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 276;
        Functions.SetArray (  ref ICONTROLID1 , (ushort)0) ; 
        __context__.SourceCodeLine = 277;
        Functions.SetArray (  ref ICONTROLID2 , (ushort)0) ; 
        __context__.SourceCodeLine = 278;
        Functions.SetArray (  ref IPHONEUNIT , (ushort)0) ; 
        __context__.SourceCodeLine = 279;
        Functions.SetArray (  ref IPHONECARD , (ushort)0) ; 
        __context__.SourceCodeLine = 280;
        Functions.SetArray (  ref IPHONELINE , (ushort)0) ; 
        __context__.SourceCodeLine = 281;
        Functions.SetArray (  ref ICONTROLTYPE , (ushort)0) ; 
        __context__.SourceCodeLine = 282;
        BPHONECONTROLINUSE = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 283;
        ISENDINFO = (ushort) ( 1 ) ; 
        __context__.SourceCodeLine = 284;
        MakeString ( TO_MODULES [ ISENDINFO] , "{0:d} {1}", (ushort)ISENDINFO, "Send Info\r" ) ; 
        __context__.SourceCodeLine = 285;
        FINFOTIMEOUT (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object SEND_NEXT_OnPush_1 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 290;
        TIMED_OUT  .Value = (ushort) ( 0 ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object SEND_NEXT_OnRelease_2 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 295;
        FSENDNEXT (  __context__  ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object GET_NEXT_INFO_OnPush_3 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 300;
        Functions.Delay (  (int) ( 1 ) ) ; 
        __context__.SourceCodeLine = 301;
        INFO_TIMED_OUT  .Value = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 302;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( ISENDINFO < 200 ) ) && Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.Not( STOP_INIT_AFTER_10_NO_REPLIES  .Value ) ) || Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( STOP_INIT_AFTER_10_NO_REPLIES  .Value ) && Functions.TestForTrue ( Functions.BoolToInt ( IINFONOANSWERCOUNT < 10 ) )) ) )) ) )) ))  ) ) 
            { 
            __context__.SourceCodeLine = 304;
            ISENDINFO = (ushort) ( (ISENDINFO + 1) ) ; 
            __context__.SourceCodeLine = 305;
            MakeString ( TO_MODULES [ ISENDINFO] , "{0:d} {1}", (ushort)ISENDINFO, "Send Info\r" ) ; 
            __context__.SourceCodeLine = 306;
            FINFOTIMEOUT (  __context__  ) ; 
            } 
        
        else 
            { 
            __context__.SourceCodeLine = 310;
            INITIALIZE_BUSY  .Value = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 311;
            BINITIALIZATIONISCOMPLETE = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 312;
            CLIENT_CONNECT  .Value = (ushort) ( 1 ) ; 
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

private void PROCESSMODULEMSG (  SplusExecutionContext __context__ ) 
    { 
    
    __context__.SourceCodeLine = 318;
    if ( Functions.TestForTrue  ( ( 0)  ) ) 
        {
        __context__.SourceCodeLine = 319;
        Trace( "sTempModules={0}\r\n", STEMPMODULES ) ; 
        }
    
    __context__.SourceCodeLine = 320;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( "Send Info" , STEMPMODULES ) > 0 ))  ) ) 
        { 
        __context__.SourceCodeLine = 322;
        CancelWait ( "WINFOTIMEOUT" ) ; 
        __context__.SourceCodeLine = 323;
        IINFONOANSWERCOUNT = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 324;
        if ( Functions.TestForTrue  ( ( 0)  ) ) 
            {
            __context__.SourceCodeLine = 325;
            Trace( "Find Send Info\r\n") ; 
            }
        
        __context__.SourceCodeLine = 326;
        ITEMPINSTANCE = (ushort) ( Functions.Atoi( STEMPMODULES ) ) ; 
        __context__.SourceCodeLine = 327;
        STRASH  .UpdateValue ( Functions.Remove ( "Send Info " , STEMPMODULES )  ) ; 
        __context__.SourceCodeLine = 328;
        ICONTROLTYPE [ ITEMPINSTANCE] = (ushort) ( Functions.Atoi( STEMPMODULES ) ) ; 
        __context__.SourceCodeLine = 329;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (ICONTROLTYPE[ ITEMPINSTANCE ] == 1) ) || Functions.TestForTrue ( Functions.BoolToInt (ICONTROLTYPE[ ITEMPINSTANCE ] == 2) )) ))  ) ) 
            { 
            __context__.SourceCodeLine = 331;
            BPHONECONTROLINUSE = (ushort) ( 1 ) ; 
            } 
        
        __context__.SourceCodeLine = 333;
        if ( Functions.TestForTrue  ( ( 0)  ) ) 
            {
            __context__.SourceCodeLine = 334;
            Trace( "iTempInstance={0:d} iControlType={1:d}\r\n", (ushort)ITEMPINSTANCE, (ushort)ICONTROLTYPE[ ITEMPINSTANCE ]) ; 
            }
        
        __context__.SourceCodeLine = 336;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ICONTROLTYPE[ ITEMPINSTANCE ] != 2))  ) ) 
            { 
            __context__.SourceCodeLine = 338;
            STRASH  .UpdateValue ( Functions.Remove ( "\u0020" , STEMPMODULES )  ) ; 
            __context__.SourceCodeLine = 339;
            ICONTROLID1 [ ITEMPINSTANCE] = (ushort) ( Functions.Atoi( STEMPMODULES ) ) ; 
            __context__.SourceCodeLine = 340;
            if ( Functions.TestForTrue  ( ( 0)  ) ) 
                {
                __context__.SourceCodeLine = 341;
                Trace( "iControlID1[{0:d}]={1:d}\r\n", (ushort)ITEMPINSTANCE, (ushort)ICONTROLID1[ ITEMPINSTANCE ]) ; 
                }
            
            __context__.SourceCodeLine = 342;
            if ( Functions.TestForTrue  ( ( Functions.Find( "\u0020" , STEMPMODULES ))  ) ) 
                { 
                __context__.SourceCodeLine = 344;
                STRASH  .UpdateValue ( Functions.Remove ( "\u0020" , STEMPMODULES )  ) ; 
                __context__.SourceCodeLine = 345;
                ICONTROLID2 [ ITEMPINSTANCE] = (ushort) ( Functions.Atoi( STEMPMODULES ) ) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 349;
                ICONTROLID2 [ ITEMPINSTANCE] = (ushort) ( 0 ) ; 
                } 
            
            __context__.SourceCodeLine = 351;
            if ( Functions.TestForTrue  ( ( 0)  ) ) 
                {
                __context__.SourceCodeLine = 352;
                Trace( "iControlID2[{0:d}]={1:d}\r\n", (ushort)ITEMPINSTANCE, (ushort)ICONTROLID2[ ITEMPINSTANCE ]) ; 
                }
            
            __context__.SourceCodeLine = 353;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ICONTROLTYPE[ ITEMPINSTANCE ] == 1))  ) ) 
                { 
                __context__.SourceCodeLine = 355;
                STRASH  .UpdateValue ( Functions.Remove ( "\u0020" , STEMPMODULES )  ) ; 
                __context__.SourceCodeLine = 356;
                IPHONEUNIT [ ITEMPINSTANCE] = (ushort) ( Functions.Atoi( STEMPMODULES ) ) ; 
                __context__.SourceCodeLine = 357;
                STRASH  .UpdateValue ( Functions.Remove ( "\u0020" , STEMPMODULES )  ) ; 
                __context__.SourceCodeLine = 358;
                IPHONECARD [ ITEMPINSTANCE] = (ushort) ( Functions.Atoi( STEMPMODULES ) ) ; 
                __context__.SourceCodeLine = 359;
                STRASH  .UpdateValue ( Functions.Remove ( "\u0020" , STEMPMODULES )  ) ; 
                __context__.SourceCodeLine = 360;
                IPHONELINE [ ITEMPINSTANCE] = (ushort) ( Functions.Atoi( STEMPMODULES ) ) ; 
                } 
            
            else 
                { 
                __context__.SourceCodeLine = 364;
                IPHONEUNIT [ ITEMPINSTANCE] = (ushort) ( 0 ) ; 
                __context__.SourceCodeLine = 365;
                IPHONECARD [ ITEMPINSTANCE] = (ushort) ( 0 ) ; 
                __context__.SourceCodeLine = 366;
                IPHONELINE [ ITEMPINSTANCE] = (ushort) ( 0 ) ; 
                } 
            
            } 
        
        else 
            { 
            __context__.SourceCodeLine = 371;
            ICONTROLID1 [ ITEMPINSTANCE] = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 372;
            ICONTROLID2 [ ITEMPINSTANCE] = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 373;
            STRASH  .UpdateValue ( Functions.Remove ( "\u0020" , STEMPMODULES )  ) ; 
            __context__.SourceCodeLine = 374;
            IPHONEUNIT [ ITEMPINSTANCE] = (ushort) ( Functions.Atoi( STEMPMODULES ) ) ; 
            __context__.SourceCodeLine = 375;
            STRASH  .UpdateValue ( Functions.Remove ( "\u0020" , STEMPMODULES )  ) ; 
            __context__.SourceCodeLine = 376;
            IPHONECARD [ ITEMPINSTANCE] = (ushort) ( Functions.Atoi( STEMPMODULES ) ) ; 
            __context__.SourceCodeLine = 377;
            IPHONELINE [ ITEMPINSTANCE] = (ushort) ( 0 ) ; 
            } 
        
        __context__.SourceCodeLine = 379;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (ICONTROLTYPE[ ITEMPINSTANCE ] != 0))  ) ) 
            { 
            __context__.SourceCodeLine = 381;
            if ( Functions.TestForTrue  ( ( 0)  ) ) 
                { 
                __context__.SourceCodeLine = 383;
                Trace( "iPhoneUnit[{0:d}]={1:d}, iPhoneCard[{2:d}]={3:d}, iPhoneLine[{4:d}]={5:d}\r\n", (ushort)ITEMPINSTANCE, (ushort)IPHONEUNIT[ ITEMPINSTANCE ], (ushort)ITEMPINSTANCE, (ushort)IPHONECARD[ ITEMPINSTANCE ], (ushort)ITEMPINSTANCE, (ushort)IPHONELINE[ ITEMPINSTANCE ]) ; 
                } 
            
            } 
        
        __context__.SourceCodeLine = 388;
        ILASTMODULECONNECTED = (ushort) ( ISENDINFO ) ; 
        __context__.SourceCodeLine = 389;
        if ( Functions.TestForTrue  ( ( 0)  ) ) 
            {
            __context__.SourceCodeLine = 390;
            Trace( "iLastModuleConnected={0:d}\r\n", (ushort)ILASTMODULECONNECTED) ; 
            }
        
        __context__.SourceCodeLine = 391;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( ISENDINFO < 200 ))  ) ) 
            { 
            __context__.SourceCodeLine = 393;
            ISENDINFO = (ushort) ( (ISENDINFO + 1) ) ; 
            __context__.SourceCodeLine = 394;
            MakeString ( TO_MODULES [ ISENDINFO] , "{0:d} {1}", (ushort)ISENDINFO, "Send Info\r" ) ; 
            __context__.SourceCodeLine = 395;
            FINFOTIMEOUT (  __context__  ) ; 
            } 
        
        else 
            { 
            __context__.SourceCodeLine = 399;
            INITIALIZE_BUSY  .Value = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 400;
            BINITIALIZATIONISCOMPLETE = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 401;
            BOKTOSEND = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 402;
            INFO_TIMED_OUT  .Value = (ushort) ( 1 ) ; 
            } 
        
        } 
    
    else 
        { 
        __context__.SourceCodeLine = 407;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( BINITIALIZATIONISCOMPLETE ) && Functions.TestForTrue ( BISCOMMUNICATING )) ))  ) ) 
            {
            __context__.SourceCodeLine = 408;
            FPROCESSMESSAGETODEVICE (  __context__ , STEMPMODULES) ; 
            }
        
        } 
    
    __context__.SourceCodeLine = 410;
    STEMPMODULES  .UpdateValue ( ""  ) ; 
    
    }
    
object FROM_MODULES_OnChange_4 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 433;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (BFLAG1 == 0))  ) ) 
            { 
            __context__.SourceCodeLine = 435;
            BFLAG1 = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 436;
            while ( Functions.TestForTrue  ( ( 1)  ) ) 
                { 
                __context__.SourceCodeLine = 438;
                STEMPMODULES  .UpdateValue ( Functions.Gather ( "\u000D" , FROM_MODULES )  ) ; 
                __context__.SourceCodeLine = 439;
                PROCESSMODULEMSG (  __context__  ) ; 
                __context__.SourceCodeLine = 436;
                } 
            
            __context__.SourceCodeLine = 441;
            BFLAG1 = (ushort) ( 0 ) ; 
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}


private void PROCESSDEVICEMSG (  SplusExecutionContext __context__ ) 
    { 
    
    __context__.SourceCodeLine = 450;
    if ( Functions.TestForTrue  ( ( 0)  ) ) 
        {
        __context__.SourceCodeLine = 451;
        Trace( "Start ProcessDeviceMsg() sTempDevice={0}\r\n", STEMPDEVICE ) ; 
        }
    
    __context__.SourceCodeLine = 452;
    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Find( "\u0023" , STEMPDEVICE ) == 1))  ) ) 
        { 
        __context__.SourceCodeLine = 454;
        if ( Functions.TestForTrue  ( ( 0)  ) ) 
            {
            __context__.SourceCodeLine = 455;
            Trace( "Find Push Response\r\n") ; 
            }
        
        __context__.SourceCodeLine = 456;
        ITEMPCONTROLID = (ushort) ( Functions.Atoi( STEMPDEVICE ) ) ; 
        __context__.SourceCodeLine = 457;
        STEMP  .UpdateValue ( Functions.Right ( STEMPDEVICE ,  (int) ( (Functions.Length( STEMPDEVICE ) - Functions.Find( "=" , STEMPDEVICE )) ) )  ) ; 
        __context__.SourceCodeLine = 458;
        ITEMPVALUE = (ushort) ( Functions.Atoi( STEMP ) ) ; 
        __context__.SourceCodeLine = 459;
        if ( Functions.TestForTrue  ( ( 0)  ) ) 
            {
            __context__.SourceCodeLine = 460;
            Trace( "iTempControlID={0:d}, iTempValue={1:d}\r\n", (ushort)ITEMPCONTROLID, (ushort)ITEMPVALUE) ; 
            }
        
        __context__.SourceCodeLine = 461;
        ushort __FN_FORSTART_VAL__1 = (ushort) ( 1 ) ;
        ushort __FN_FOREND_VAL__1 = (ushort)ILASTMODULECONNECTED; 
        int __FN_FORSTEP_VAL__1 = (int)1; 
        for ( A  = __FN_FORSTART_VAL__1; (__FN_FORSTEP_VAL__1 > 0)  ? ( (A  >= __FN_FORSTART_VAL__1) && (A  <= __FN_FOREND_VAL__1) ) : ( (A  <= __FN_FORSTART_VAL__1) && (A  >= __FN_FOREND_VAL__1) ) ; A  += (ushort)__FN_FORSTEP_VAL__1) 
            { 
            __context__.SourceCodeLine = 463;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (ICONTROLTYPE[ A ] == 0) ) && Functions.TestForTrue ( Functions.BoolToInt ( ICONTROLID2[ A ] > 0 ) )) ) ) && Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( ICONTROLID1[ A ] <= ITEMPCONTROLID ) ) && Functions.TestForTrue ( Functions.BoolToInt ( ICONTROLID2[ A ] >= ITEMPCONTROLID ) )) ) )) ))  ) ) 
                { 
                __context__.SourceCodeLine = 465;
                TO_MODULES [ A]  .UpdateValue ( STEMPDEVICE  ) ; 
                __context__.SourceCodeLine = 466;
                break ; 
                } 
            
            else 
                {
                __context__.SourceCodeLine = 468;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (ICONTROLTYPE[ A ] == 0) ) && Functions.TestForTrue ( Functions.BoolToInt (ICONTROLID2[ A ] == 0) )) ) ) && Functions.TestForTrue ( Functions.BoolToInt ( ICONTROLID1[ A ] > 0 ) )) ) ) && Functions.TestForTrue ( Functions.BoolToInt (ICONTROLID1[ A ] == ITEMPCONTROLID) )) ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 470;
                    TO_MODULES [ A]  .UpdateValue ( STEMPDEVICE  ) ; 
                    __context__.SourceCodeLine = 471;
                    break ; 
                    } 
                
                else 
                    {
                    __context__.SourceCodeLine = 473;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (ICONTROLTYPE[ A ] == 1) ) && Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( ICONTROLID1[ A ] <= ITEMPCONTROLID ) ) && Functions.TestForTrue ( Functions.BoolToInt ( ICONTROLID2[ A ] >= ITEMPCONTROLID ) )) ) )) ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 475;
                        TO_MODULES [ A]  .UpdateValue ( STEMPDEVICE  ) ; 
                        __context__.SourceCodeLine = 476;
                        break ; 
                        } 
                    
                    }
                
                }
            
            __context__.SourceCodeLine = 461;
            } 
        
        } 
    
    else 
        {
        __context__.SourceCodeLine = 480;
        if ( Functions.TestForTrue  ( ( Functions.Find( "ACK\r" , STEMPDEVICE ))  ) ) 
            { 
            __context__.SourceCodeLine = 482;
            if ( Functions.TestForTrue  ( ( 0)  ) ) 
                {
                __context__.SourceCodeLine = 483;
                Trace( "ACK in, sLastSent={0}\r\n", SLASTSENT ) ; 
                }
            
            __context__.SourceCodeLine = 484;
            CancelWait ( "WTIMEOUT" ) ; 
            __context__.SourceCodeLine = 485;
            IDEVICEMESSAGETIMEOUTCOUNT = (ushort) ( 0 ) ; 
            __context__.SourceCodeLine = 486;
            if ( Functions.TestForTrue  ( ( Functions.Find( SLASTSENT , STEMPDEVICE ))  ) ) 
                { 
                __context__.SourceCodeLine = 488;
                if ( Functions.TestForTrue  ( ( 0)  ) ) 
                    {
                    __context__.SourceCodeLine = 489;
                    Trace( "Last Command ACK in\r\n") ; 
                    }
                
                __context__.SourceCodeLine = 490;
                Functions.Delay (  (int) ( 5 ) ) ; 
                __context__.SourceCodeLine = 491;
                BOKTOSEND = (ushort) ( 1 ) ; 
                __context__.SourceCodeLine = 492;
                SLASTSENT  .UpdateValue ( ""  ) ; 
                __context__.SourceCodeLine = 493;
                FSENDNEXT (  __context__  ) ; 
                __context__.SourceCodeLine = 495;
                if ( Functions.TestForTrue  ( ( Functions.Find( "{SSYSS" , STEMPDEVICE ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 497;
                    if ( Functions.TestForTrue  ( ( 0)  ) ) 
                        {
                        __context__.SourceCodeLine = 498;
                        Trace( "Last Command SSYSS ACK in\r\n") ; 
                        }
                    
                    __context__.SourceCodeLine = 499;
                    STRASH  .UpdateValue ( Functions.Remove ( "SSYSS " , STEMPDEVICE )  ) ; 
                    __context__.SourceCodeLine = 500;
                    ITEMPUNIT = (ushort) ( Functions.Atoi( STEMPDEVICE ) ) ; 
                    __context__.SourceCodeLine = 501;
                    STRASH  .UpdateValue ( Functions.Remove ( "." , STEMPDEVICE )  ) ; 
                    __context__.SourceCodeLine = 502;
                    ITEMPRESOURCE = (ushort) ( Functions.Atoi( STEMPDEVICE ) ) ; 
                    __context__.SourceCodeLine = 503;
                    STRASH  .UpdateValue ( Functions.Remove ( "." , STEMPDEVICE )  ) ; 
                    __context__.SourceCodeLine = 504;
                    ITEMPENUM = (ushort) ( Functions.Atoi( STEMPDEVICE ) ) ; 
                    __context__.SourceCodeLine = 505;
                    STRASH  .UpdateValue ( Functions.Remove ( "." , STEMPDEVICE )  ) ; 
                    __context__.SourceCodeLine = 506;
                    ITEMPCARD = (ushort) ( Functions.Atoi( STEMPDEVICE ) ) ; 
                    __context__.SourceCodeLine = 507;
                    STRASH  .UpdateValue ( Functions.Remove ( "." , STEMPDEVICE )  ) ; 
                    __context__.SourceCodeLine = 508;
                    ITEMPCHANNEL = (ushort) ( Functions.Atoi( STEMPDEVICE ) ) ; 
                    __context__.SourceCodeLine = 509;
                    STRASH  .UpdateValue ( Functions.Remove ( "=" , STEMPDEVICE )  ) ; 
                    __context__.SourceCodeLine = 510;
                    SPHONEDATA  .UpdateValue ( Functions.Left ( STEMPDEVICE ,  (int) ( (Functions.Find( "}" , STEMPDEVICE ) - 1) ) )  ) ; 
                    __context__.SourceCodeLine = 511;
                    if ( Functions.TestForTrue  ( ( 0)  ) ) 
                        {
                        __context__.SourceCodeLine = 512;
                        Trace( "iTempUnit={0:d}, iTempResource={1:d}, iTempEnum={2:d}, iTempCard={3:d}, iTempChannel={4:d}, sPhoneData={5}\r\n", (ushort)ITEMPUNIT, (ushort)ITEMPRESOURCE, (ushort)ITEMPENUM, (ushort)ITEMPCARD, (ushort)ITEMPCHANNEL, SPHONEDATA ) ; 
                        }
                    
                    __context__.SourceCodeLine = 513;
                    ushort __FN_FORSTART_VAL__2 = (ushort) ( 1 ) ;
                    ushort __FN_FOREND_VAL__2 = (ushort)ILASTMODULECONNECTED; 
                    int __FN_FORSTEP_VAL__2 = (int)1; 
                    for ( A  = __FN_FORSTART_VAL__2; (__FN_FORSTEP_VAL__2 > 0)  ? ( (A  >= __FN_FORSTART_VAL__2) && (A  <= __FN_FOREND_VAL__2) ) : ( (A  <= __FN_FORSTART_VAL__2) && (A  >= __FN_FOREND_VAL__2) ) ; A  += (ushort)__FN_FORSTEP_VAL__2) 
                        { 
                        __context__.SourceCodeLine = 515;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (ICONTROLTYPE[ A ] == 1) ) && Functions.TestForTrue ( Functions.BoolToInt (IPHONEUNIT[ A ] == ITEMPUNIT) )) ) ) && Functions.TestForTrue ( Functions.BoolToInt (IPHONECARD[ A ] == ITEMPCARD) )) ) ) && Functions.TestForTrue ( Functions.BoolToInt ( ITEMPRESOURCE > 1001 ) )) ) ) && Functions.TestForTrue ( Functions.BoolToInt (IPHONELINE[ A ] == ITEMPCHANNEL) )) ))  ) ) 
                            { 
                            __context__.SourceCodeLine = 517;
                            MakeString ( TO_MODULES [ A] , "SSYS {0:d}.{1:d}.{2:d}.{3:d}.{4:d}={5}\r", (ushort)ITEMPUNIT, (ushort)ITEMPRESOURCE, (ushort)ITEMPENUM, (ushort)ITEMPCARD, (ushort)ITEMPCHANNEL, SPHONEDATA ) ; 
                            __context__.SourceCodeLine = 518;
                            break ; 
                            } 
                        
                        else 
                            {
                            __context__.SourceCodeLine = 520;
                            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (ICONTROLTYPE[ A ] == 2) ) && Functions.TestForTrue ( Functions.BoolToInt (IPHONEUNIT[ A ] == ITEMPUNIT) )) ) ) && Functions.TestForTrue ( Functions.BoolToInt (IPHONECARD[ A ] == ITEMPCARD) )) ) ) && Functions.TestForTrue ( Functions.BoolToInt ( ITEMPRESOURCE <= 1001 ) )) ))  ) ) 
                                { 
                                __context__.SourceCodeLine = 522;
                                MakeString ( TO_MODULES [ A] , "SSYS {0:d}.{1:d}.{2:d}.{3:d}.{4:d}={5}\r", (ushort)ITEMPUNIT, (ushort)ITEMPRESOURCE, (ushort)ITEMPENUM, (ushort)ITEMPCARD, (ushort)ITEMPCHANNEL, SPHONEDATA ) ; 
                                __context__.SourceCodeLine = 523;
                                break ; 
                                } 
                            
                            }
                        
                        __context__.SourceCodeLine = 513;
                        } 
                    
                    } 
                
                } 
            
            } 
        
        else 
            {
            __context__.SourceCodeLine = 531;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.Find( "{GSYSS" , STEMPDEVICE ) ) || Functions.TestForTrue ( Functions.BoolToInt ( Functions.Find( ".100" , STEMPDEVICE ) < Functions.Find( "=" , STEMPDEVICE ) ) )) ))  ) ) 
                { 
                __context__.SourceCodeLine = 533;
                if ( Functions.TestForTrue  ( ( Functions.Find( "{GSYSS" , STEMPDEVICE ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 535;
                    if ( Functions.TestForTrue  ( ( Functions.Find( SLASTSENT , STEMPDEVICE ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 537;
                        if ( Functions.TestForTrue  ( ( 0)  ) ) 
                            {
                            __context__.SourceCodeLine = 538;
                            Trace( "Last Response GSYSS ACK in\r\n") ; 
                            }
                        
                        __context__.SourceCodeLine = 539;
                        Functions.Delay (  (int) ( 5 ) ) ; 
                        __context__.SourceCodeLine = 540;
                        BOKTOSEND = (ushort) ( 1 ) ; 
                        __context__.SourceCodeLine = 541;
                        SLASTSENT  .UpdateValue ( ""  ) ; 
                        __context__.SourceCodeLine = 542;
                        FSENDNEXT (  __context__  ) ; 
                        } 
                    
                    __context__.SourceCodeLine = 545;
                    STRASH  .UpdateValue ( Functions.Remove ( "GSYSS " , STEMPDEVICE )  ) ; 
                    } 
                
                else 
                    { 
                    __context__.SourceCodeLine = 549;
                    if ( Functions.TestForTrue  ( ( 0)  ) ) 
                        {
                        __context__.SourceCodeLine = 550;
                        Trace( "Last Response SYSS Push Response in\r\n") ; 
                        }
                    
                    __context__.SourceCodeLine = 551;
                    STRASH  .UpdateValue ( Functions.Remove ( "} " , STEMPDEVICE )  ) ; 
                    } 
                
                __context__.SourceCodeLine = 553;
                ITEMPUNIT = (ushort) ( Functions.Atoi( STEMPDEVICE ) ) ; 
                __context__.SourceCodeLine = 554;
                STRASH  .UpdateValue ( Functions.Remove ( "." , STEMPDEVICE )  ) ; 
                __context__.SourceCodeLine = 555;
                ITEMPRESOURCE = (ushort) ( Functions.Atoi( STEMPDEVICE ) ) ; 
                __context__.SourceCodeLine = 556;
                STRASH  .UpdateValue ( Functions.Remove ( "." , STEMPDEVICE )  ) ; 
                __context__.SourceCodeLine = 557;
                ITEMPENUM = (ushort) ( Functions.Atoi( STEMPDEVICE ) ) ; 
                __context__.SourceCodeLine = 558;
                STRASH  .UpdateValue ( Functions.Remove ( "." , STEMPDEVICE )  ) ; 
                __context__.SourceCodeLine = 559;
                ITEMPCARD = (ushort) ( Functions.Atoi( STEMPDEVICE ) ) ; 
                __context__.SourceCodeLine = 560;
                STRASH  .UpdateValue ( Functions.Remove ( "." , STEMPDEVICE )  ) ; 
                __context__.SourceCodeLine = 561;
                ITEMPCHANNEL = (ushort) ( Functions.Atoi( STEMPDEVICE ) ) ; 
                __context__.SourceCodeLine = 562;
                if ( Functions.TestForTrue  ( ( Functions.Find( "} " , STEMPDEVICE ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 564;
                    STRASH  .UpdateValue ( Functions.Remove ( "} " , STEMPDEVICE )  ) ; 
                    } 
                
                else 
                    {
                    __context__.SourceCodeLine = 566;
                    if ( Functions.TestForTrue  ( ( Functions.Find( "=" , STEMPDEVICE ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 568;
                        STRASH  .UpdateValue ( Functions.Remove ( "=" , STEMPDEVICE )  ) ; 
                        } 
                    
                    }
                
                __context__.SourceCodeLine = 571;
                SPHONEDATA  .UpdateValue ( Functions.Left ( STEMPDEVICE ,  (int) ( (Functions.Length( STEMPDEVICE ) - 1) ) )  ) ; 
                __context__.SourceCodeLine = 572;
                if ( Functions.TestForTrue  ( ( 0)  ) ) 
                    {
                    __context__.SourceCodeLine = 573;
                    Trace( "iTempUnit={0:d}, iTempResource={1:d}, iTempEnum={2:d}, iTempCard={3:d}, iTempChannel={4:d}, sPhoneData={5}\r\n", (ushort)ITEMPUNIT, (ushort)ITEMPRESOURCE, (ushort)ITEMPENUM, (ushort)ITEMPCARD, (ushort)ITEMPCHANNEL, SPHONEDATA ) ; 
                    }
                
                __context__.SourceCodeLine = 574;
                ushort __FN_FORSTART_VAL__3 = (ushort) ( 1 ) ;
                ushort __FN_FOREND_VAL__3 = (ushort)ILASTMODULECONNECTED; 
                int __FN_FORSTEP_VAL__3 = (int)1; 
                for ( A  = __FN_FORSTART_VAL__3; (__FN_FORSTEP_VAL__3 > 0)  ? ( (A  >= __FN_FORSTART_VAL__3) && (A  <= __FN_FOREND_VAL__3) ) : ( (A  <= __FN_FORSTART_VAL__3) && (A  >= __FN_FOREND_VAL__3) ) ; A  += (ushort)__FN_FORSTEP_VAL__3) 
                    { 
                    __context__.SourceCodeLine = 576;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (ICONTROLTYPE[ A ] == 1) ) && Functions.TestForTrue ( Functions.BoolToInt (IPHONEUNIT[ A ] == ITEMPUNIT) )) ) ) && Functions.TestForTrue ( Functions.BoolToInt (IPHONECARD[ A ] == ITEMPCARD) )) ) ) && Functions.TestForTrue ( Functions.BoolToInt ( ITEMPRESOURCE > 1001 ) )) ) ) && Functions.TestForTrue ( Functions.BoolToInt (IPHONELINE[ A ] == ITEMPCHANNEL) )) ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 578;
                        MakeString ( TO_MODULES [ A] , "SSYS {0:d}.{1:d}.{2:d}.{3:d}.{4:d}={5}\r", (ushort)ITEMPUNIT, (ushort)ITEMPRESOURCE, (ushort)ITEMPENUM, (ushort)ITEMPCARD, (ushort)ITEMPCHANNEL, SPHONEDATA ) ; 
                        __context__.SourceCodeLine = 579;
                        break ; 
                        } 
                    
                    else 
                        {
                        __context__.SourceCodeLine = 581;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt ( (Functions.TestForTrue ( Functions.BoolToInt (ICONTROLTYPE[ A ] == 2) ) && Functions.TestForTrue ( Functions.BoolToInt (IPHONEUNIT[ A ] == ITEMPUNIT) )) ) ) && Functions.TestForTrue ( Functions.BoolToInt (IPHONECARD[ A ] == ITEMPCARD) )) ) ) && Functions.TestForTrue ( Functions.BoolToInt ( ITEMPRESOURCE <= 1001 ) )) ))  ) ) 
                            { 
                            __context__.SourceCodeLine = 583;
                            MakeString ( TO_MODULES [ A] , "SSYS {0:d}.{1:d}.{2:d}.{3:d}.{4:d}={5}\r", (ushort)ITEMPUNIT, (ushort)ITEMPRESOURCE, (ushort)ITEMPENUM, (ushort)ITEMPCARD, (ushort)ITEMPCHANNEL, SPHONEDATA ) ; 
                            __context__.SourceCodeLine = 584;
                            break ; 
                            } 
                        
                        }
                    
                    __context__.SourceCodeLine = 574;
                    } 
                
                } 
            
            else 
                {
                __context__.SourceCodeLine = 590;
                if ( Functions.TestForTrue  ( ( Functions.Find( "NAK\r" , STEMPDEVICE ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 592;
                    CancelWait ( "WTIMEOUT" ) ; 
                    __context__.SourceCodeLine = 593;
                    IDEVICEMESSAGETIMEOUTCOUNT = (ushort) ( 0 ) ; 
                    __context__.SourceCodeLine = 594;
                    if ( Functions.TestForTrue  ( ( 0)  ) ) 
                        {
                        __context__.SourceCodeLine = 595;
                        Trace( "NAK in\r\n") ; 
                        }
                    
                    __context__.SourceCodeLine = 596;
                    IRESENDCOUNT = (ushort) ( (IRESENDCOUNT + 1) ) ; 
                    __context__.SourceCodeLine = 597;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( IRESENDCOUNT < 2 ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 599;
                        FTIMEOUT (  __context__  ) ; 
                        __context__.SourceCodeLine = 600;
                        TO_DEVICE  .UpdateValue ( "$e " + SLASTSENT + "\r"  ) ; 
                        __context__.SourceCodeLine = 601;
                        if ( Functions.TestForTrue  ( ( 0)  ) ) 
                            {
                            __context__.SourceCodeLine = 602;
                            Trace( "To Device=$e {0}\r sLastSent={1}\n", SLASTSENT , SLASTSENT ) ; 
                            }
                        
                        } 
                    
                    else 
                        { 
                        __context__.SourceCodeLine = 606;
                        Trace( "Send failed due to NAK count\r\n") ; 
                        __context__.SourceCodeLine = 607;
                        SLASTSENT  .UpdateValue ( ""  ) ; 
                        __context__.SourceCodeLine = 608;
                        BOKTOSEND = (ushort) ( 1 ) ; 
                        __context__.SourceCodeLine = 609;
                        FSENDNEXT (  __context__  ) ; 
                        } 
                    
                    } 
                
                }
            
            }
        
        }
    
    __context__.SourceCodeLine = 612;
    FSETISCOMMUNICATING (  __context__  ) ; 
    __context__.SourceCodeLine = 613;
    STEMPDEVICE  .UpdateValue ( ""  ) ; 
    
    }
    
object FROM_DEVICE_OnChange_5 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 636;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (BFLAG2 == 0))  ) ) 
            { 
            __context__.SourceCodeLine = 638;
            BFLAG2 = (ushort) ( 1 ) ; 
            __context__.SourceCodeLine = 639;
            while ( Functions.TestForTrue  ( ( 1)  ) ) 
                { 
                __context__.SourceCodeLine = 641;
                STEMPDEVICE  .UpdateValue ( Functions.Gather ( "\u000D" , FROM_DEVICE )  ) ; 
                __context__.SourceCodeLine = 642;
                PROCESSDEVICEMSG (  __context__  ) ; 
                __context__.SourceCodeLine = 639;
                } 
            
            __context__.SourceCodeLine = 644;
            BFLAG2 = (ushort) ( 0 ) ; 
            } 
        
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}


object CLIENT_CONNECT_FB_OnRelease_6 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 652;
        BISCOMMUNICATING = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 653;
        Trace( "TCP IP Client disconnected - Symnet device not communicating\r\n") ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler( __SignalEventArg__ ); }
    return this;
    
}

object CLIENT_CONNECT_STATUS_FB_OnChange_7 ( Object __EventInfo__ )

    { 
    Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
    try
    {
        SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
        
        __context__.SourceCodeLine = 658;
        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (CLIENT_CONNECT_STATUS_FB  .UshortValue == 2))  ) ) 
            {
            __context__.SourceCodeLine = 659;
            FPROCESSMESSAGETODEVICE (  __context__ , "NOP\r") ; 
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
        
        __context__.SourceCodeLine = 670;
        STEMPMODULES  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 671;
        STEMPDEVICE  .UpdateValue ( ""  ) ; 
        __context__.SourceCodeLine = 672;
        Functions.SetArray ( SCOMMAND , "" ) ; 
        __context__.SourceCodeLine = 673;
        BINITIALIZATIONISCOMPLETE = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 674;
        BOKTOSEND = (ushort) ( 1 ) ; 
        __context__.SourceCodeLine = 675;
        BFLAG1 = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 676;
        BFLAG2 = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 677;
        IRESENDCOUNT = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 678;
        IDEVICEMESSAGETIMEOUTCOUNT = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 679;
        BISCOMMUNICATING = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 680;
        CLIENT_CONNECT  .Value = (ushort) ( 0 ) ; 
        __context__.SourceCodeLine = 681;
        BPHONECONTROLINUSE = (ushort) ( 0 ) ; 
        
        
    }
    catch(Exception e) { ObjectCatchHandler(e); }
    finally { ObjectFinallyHandler(); }
    return __obj__;
    }
    

public override void LogosSplusInitialize()
{
    _SplusNVRAM = new SplusNVRAM( this );
    ICONTROLID1  = new ushort[ 201 ];
    ICONTROLID2  = new ushort[ 201 ];
    ICONTROLTYPE  = new ushort[ 201 ];
    IPHONEUNIT  = new ushort[ 201 ];
    IPHONECARD  = new ushort[ 201 ];
    IPHONELINE  = new ushort[ 201 ];
    STEMPMODULES  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 100, this );
    STEMPDEVICE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 100, this );
    STRASH  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 100, this );
    STEMP  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 100, this );
    SLASTSENT  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 100, this );
    SPHONEDATA  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 100, this );
    SCOMMAND  = new CrestronString[ 501 ];
    for( uint i = 0; i < 501; i++ )
        SCOMMAND [i] = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 100, this );
    
    INITIALIZE = new Crestron.Logos.SplusObjects.DigitalInput( INITIALIZE__DigitalInput__, this );
    m_DigitalInputList.Add( INITIALIZE__DigitalInput__, INITIALIZE );
    
    GET_NEXT_INFO = new Crestron.Logos.SplusObjects.DigitalInput( GET_NEXT_INFO__DigitalInput__, this );
    m_DigitalInputList.Add( GET_NEXT_INFO__DigitalInput__, GET_NEXT_INFO );
    
    SEND_NEXT = new Crestron.Logos.SplusObjects.DigitalInput( SEND_NEXT__DigitalInput__, this );
    m_DigitalInputList.Add( SEND_NEXT__DigitalInput__, SEND_NEXT );
    
    STOP_INIT_AFTER_10_NO_REPLIES = new Crestron.Logos.SplusObjects.DigitalInput( STOP_INIT_AFTER_10_NO_REPLIES__DigitalInput__, this );
    m_DigitalInputList.Add( STOP_INIT_AFTER_10_NO_REPLIES__DigitalInput__, STOP_INIT_AFTER_10_NO_REPLIES );
    
    CLIENT_CONNECT_FB = new Crestron.Logos.SplusObjects.DigitalInput( CLIENT_CONNECT_FB__DigitalInput__, this );
    m_DigitalInputList.Add( CLIENT_CONNECT_FB__DigitalInput__, CLIENT_CONNECT_FB );
    
    INITIALIZE_BUSY = new Crestron.Logos.SplusObjects.DigitalOutput( INITIALIZE_BUSY__DigitalOutput__, this );
    m_DigitalOutputList.Add( INITIALIZE_BUSY__DigitalOutput__, INITIALIZE_BUSY );
    
    INFO_TIMED_OUT = new Crestron.Logos.SplusObjects.DigitalOutput( INFO_TIMED_OUT__DigitalOutput__, this );
    m_DigitalOutputList.Add( INFO_TIMED_OUT__DigitalOutput__, INFO_TIMED_OUT );
    
    TIMED_OUT = new Crestron.Logos.SplusObjects.DigitalOutput( TIMED_OUT__DigitalOutput__, this );
    m_DigitalOutputList.Add( TIMED_OUT__DigitalOutput__, TIMED_OUT );
    
    CLIENT_CONNECT = new Crestron.Logos.SplusObjects.DigitalOutput( CLIENT_CONNECT__DigitalOutput__, this );
    m_DigitalOutputList.Add( CLIENT_CONNECT__DigitalOutput__, CLIENT_CONNECT );
    
    CLIENT_CONNECT_STATUS_FB = new Crestron.Logos.SplusObjects.AnalogInput( CLIENT_CONNECT_STATUS_FB__AnalogSerialInput__, this );
    m_AnalogInputList.Add( CLIENT_CONNECT_STATUS_FB__AnalogSerialInput__, CLIENT_CONNECT_STATUS_FB );
    
    TO_DEVICE = new Crestron.Logos.SplusObjects.StringOutput( TO_DEVICE__AnalogSerialOutput__, this );
    m_StringOutputList.Add( TO_DEVICE__AnalogSerialOutput__, TO_DEVICE );
    
    TO_MODULES = new InOutArray<StringOutput>( 200, this );
    for( uint i = 0; i < 200; i++ )
    {
        TO_MODULES[i+1] = new Crestron.Logos.SplusObjects.StringOutput( TO_MODULES__AnalogSerialOutput__ + i, this );
        m_StringOutputList.Add( TO_MODULES__AnalogSerialOutput__ + i, TO_MODULES[i+1] );
    }
    
    FROM_DEVICE = new Crestron.Logos.SplusObjects.BufferInput( FROM_DEVICE__AnalogSerialInput__, 5000, this );
    m_StringInputList.Add( FROM_DEVICE__AnalogSerialInput__, FROM_DEVICE );
    
    FROM_MODULES = new Crestron.Logos.SplusObjects.BufferInput( FROM_MODULES__AnalogSerialInput__, 5000, this );
    m_StringInputList.Add( FROM_MODULES__AnalogSerialInput__, FROM_MODULES );
    
    WTIMEOUT_Callback = new WaitFunction( WTIMEOUT_CallbackFn );
    HEARTBEAT_Callback = new WaitFunction( HEARTBEAT_CallbackFn );
    WINFOTIMEOUT_Callback = new WaitFunction( WINFOTIMEOUT_CallbackFn );
    
    INITIALIZE.OnDigitalPush.Add( new InputChangeHandlerWrapper( INITIALIZE_OnPush_0, false ) );
    SEND_NEXT.OnDigitalPush.Add( new InputChangeHandlerWrapper( SEND_NEXT_OnPush_1, false ) );
    SEND_NEXT.OnDigitalRelease.Add( new InputChangeHandlerWrapper( SEND_NEXT_OnRelease_2, false ) );
    GET_NEXT_INFO.OnDigitalPush.Add( new InputChangeHandlerWrapper( GET_NEXT_INFO_OnPush_3, false ) );
    FROM_MODULES.OnSerialChange.Add( new InputChangeHandlerWrapper( FROM_MODULES_OnChange_4, true ) );
    FROM_DEVICE.OnSerialChange.Add( new InputChangeHandlerWrapper( FROM_DEVICE_OnChange_5, true ) );
    CLIENT_CONNECT_FB.OnDigitalRelease.Add( new InputChangeHandlerWrapper( CLIENT_CONNECT_FB_OnRelease_6, false ) );
    CLIENT_CONNECT_STATUS_FB.OnAnalogChange.Add( new InputChangeHandlerWrapper( CLIENT_CONNECT_STATUS_FB_OnChange_7, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_SYMETRIX_COMMAND_PROCESSOR_SIMPL__IP_V3_0 ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}


private WaitFunction WTIMEOUT_Callback;
private WaitFunction HEARTBEAT_Callback;
private WaitFunction WINFOTIMEOUT_Callback;


const uint INITIALIZE__DigitalInput__ = 0;
const uint GET_NEXT_INFO__DigitalInput__ = 1;
const uint SEND_NEXT__DigitalInput__ = 2;
const uint STOP_INIT_AFTER_10_NO_REPLIES__DigitalInput__ = 3;
const uint CLIENT_CONNECT_FB__DigitalInput__ = 4;
const uint CLIENT_CONNECT_STATUS_FB__AnalogSerialInput__ = 0;
const uint FROM_DEVICE__AnalogSerialInput__ = 1;
const uint FROM_MODULES__AnalogSerialInput__ = 2;
const uint INITIALIZE_BUSY__DigitalOutput__ = 0;
const uint INFO_TIMED_OUT__DigitalOutput__ = 1;
const uint TIMED_OUT__DigitalOutput__ = 2;
const uint CLIENT_CONNECT__DigitalOutput__ = 3;
const uint TO_DEVICE__AnalogSerialOutput__ = 0;
const uint TO_MODULES__AnalogSerialOutput__ = 1;

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
