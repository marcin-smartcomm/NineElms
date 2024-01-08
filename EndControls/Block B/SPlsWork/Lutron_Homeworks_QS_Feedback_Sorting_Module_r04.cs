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

namespace UserModule_LUTRON_HOMEWORKS_QS_FEEDBACK_SORTING_MODULE_R04
{
    public class UserModuleClass_LUTRON_HOMEWORKS_QS_FEEDBACK_SORTING_MODULE_R04 : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        
        
        Crestron.Logos.SplusObjects.DigitalInput SHOW_TRACE_MSGS;
        Crestron.Logos.SplusObjects.BufferInput FROM_CORE_MODULE__DOLLAR__;
        InOutArray<Crestron.Logos.SplusObjects.DigitalOutput> ERROR;
        Crestron.Logos.SplusObjects.StringOutput MONITORING__DOLLAR__;
        InOutArray<Crestron.Logos.SplusObjects.StringOutput> INTEGRATION_ID_GROUP__DOLLAR__;
        private void PARSE (  SplusExecutionContext __context__ ) 
            { 
            ushort INT_ID = 0;
            ushort INT_ID_GROUP = 0;
            ushort ERROR_ID = 0;
            
            
            __context__.SourceCodeLine = 51;
            if ( Functions.TestForTrue  ( ( SHOW_TRACE_MSGS  .Value)  ) ) 
                {
                __context__.SourceCodeLine = 52;
                Trace( "GATHERED = {0}\r\n", _SplusNVRAM.PARSEDMESSAGE ) ; 
                }
            
            __context__.SourceCodeLine = 54;
            while ( Functions.TestForTrue  ( ( Functions.Find( "QNET> " , _SplusNVRAM.PARSEDMESSAGE ))  ) ) 
                { 
                __context__.SourceCodeLine = 56;
                _SplusNVRAM.PARSEDTRASH  .UpdateValue ( Functions.Remove ( "QNET> " , _SplusNVRAM.PARSEDMESSAGE )  ) ; 
                __context__.SourceCodeLine = 57;
                if ( Functions.TestForTrue  ( ( SHOW_TRACE_MSGS  .Value)  ) ) 
                    {
                    __context__.SourceCodeLine = 58;
                    Trace( "QNET> REMOVED, PARSEDMESSAGE = {0}\r\n", _SplusNVRAM.PARSEDMESSAGE ) ; 
                    }
                
                __context__.SourceCodeLine = 54;
                } 
            
            __context__.SourceCodeLine = 60;
            if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Find( "~MONITORING" , _SplusNVRAM.PARSEDMESSAGE ) == 1))  ) ) 
                { 
                __context__.SourceCodeLine = 62;
                MONITORING__DOLLAR__  .UpdateValue ( _SplusNVRAM.PARSEDMESSAGE  ) ; 
                } 
            
            else 
                {
                __context__.SourceCodeLine = 64;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Find( "~SYSTEM" , _SplusNVRAM.PARSEDMESSAGE ) == 1))  ) ) 
                    { 
                    __context__.SourceCodeLine = 66;
                    if ( Functions.TestForTrue  ( ( SHOW_TRACE_MSGS  .Value)  ) ) 
                        {
                        __context__.SourceCodeLine = 67;
                        Trace( "SYSTEM RESPONSE RECEIVED, PARSEDMESSAGE = {0}\r\n", _SplusNVRAM.PARSEDMESSAGE ) ; 
                        }
                    
                    } 
                
                else 
                    {
                    __context__.SourceCodeLine = 69;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Find( "~ERROR" , _SplusNVRAM.PARSEDMESSAGE ) == 1))  ) ) 
                        { 
                        __context__.SourceCodeLine = 71;
                        ERROR_ID = (ushort) ( Functions.Atoi( _SplusNVRAM.PARSEDMESSAGE ) ) ; 
                        __context__.SourceCodeLine = 72;
                        Functions.Pulse ( 10, ERROR [ ERROR_ID] ) ; 
                        } 
                    
                    else 
                        {
                        __context__.SourceCodeLine = 74;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Find( "~" , _SplusNVRAM.PARSEDMESSAGE ) == 1))  ) ) 
                            { 
                            __context__.SourceCodeLine = 76;
                            INT_ID = (ushort) ( Functions.Atoi( _SplusNVRAM.PARSEDMESSAGE ) ) ; 
                            __context__.SourceCodeLine = 77;
                            INT_ID_GROUP = (ushort) ( (((INT_ID - 1) / 200) + 1) ) ; 
                            __context__.SourceCodeLine = 79;
                            if ( Functions.TestForTrue  ( ( IsSignalDefined( INTEGRATION_ID_GROUP__DOLLAR__[ INT_ID_GROUP ] ))  ) ) 
                                {
                                __context__.SourceCodeLine = 80;
                                INTEGRATION_ID_GROUP__DOLLAR__ [ INT_ID_GROUP]  .UpdateValue ( _SplusNVRAM.PARSEDMESSAGE  ) ; 
                                }
                            
                            else 
                                {
                                __context__.SourceCodeLine = 82;
                                Print( "INTEGRATION ID GROUP {0:d} NOT DEFINED ON LUTRON FEEDBACK SORTER\r\n", (short)INT_ID_GROUP) ; 
                                }
                            
                            } 
                        
                        }
                    
                    }
                
                }
            
            
            }
            
        object FROM_CORE_MODULE__DOLLAR___OnChange_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 115;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (_SplusNVRAM.PROCESSINGBUSY == 0))  ) ) 
                    { 
                    __context__.SourceCodeLine = 117;
                    _SplusNVRAM.PROCESSINGBUSY = (ushort) ( 1 ) ; 
                    __context__.SourceCodeLine = 119;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( "\u0000" , FROM_CORE_MODULE__DOLLAR__ ) > 0 ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 121;
                        _SplusNVRAM.PARSEDTRASH  .UpdateValue ( Functions.Remove ( "\u0000" , FROM_CORE_MODULE__DOLLAR__ )  ) ; 
                        } 
                    
                    __context__.SourceCodeLine = 124;
                    while ( Functions.TestForTrue  ( ( 1)  ) ) 
                        { 
                        __context__.SourceCodeLine = 126;
                        _SplusNVRAM.PARSEDMESSAGE  .UpdateValue ( Functions.Gather ( "\r\n" , FROM_CORE_MODULE__DOLLAR__ )  ) ; 
                        __context__.SourceCodeLine = 127;
                        PARSE (  __context__  ) ; 
                        __context__.SourceCodeLine = 124;
                        } 
                    
                    __context__.SourceCodeLine = 130;
                    _SplusNVRAM.PROCESSINGBUSY = (ushort) ( 0 ) ; 
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
            
            __context__.SourceCodeLine = 142;
            WaitForInitializationComplete ( ) ; 
            __context__.SourceCodeLine = 143;
            Functions.ClearBuffer ( FROM_CORE_MODULE__DOLLAR__ ) ; 
            __context__.SourceCodeLine = 144;
            _SplusNVRAM.PROCESSINGBUSY = (ushort) ( 0 ) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler(); }
        return __obj__;
        }
        
    
    public override void LogosSplusInitialize()
    {
        _SplusNVRAM = new SplusNVRAM( this );
        _SplusNVRAM.PARSEDMESSAGE  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 100, this );
        _SplusNVRAM.PARSEDTRASH  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 100, this );
        
        SHOW_TRACE_MSGS = new Crestron.Logos.SplusObjects.DigitalInput( SHOW_TRACE_MSGS__DigitalInput__, this );
        m_DigitalInputList.Add( SHOW_TRACE_MSGS__DigitalInput__, SHOW_TRACE_MSGS );
        
        ERROR = new InOutArray<DigitalOutput>( 5, this );
        for( uint i = 0; i < 5; i++ )
        {
            ERROR[i+1] = new Crestron.Logos.SplusObjects.DigitalOutput( ERROR__DigitalOutput__ + i, this );
            m_DigitalOutputList.Add( ERROR__DigitalOutput__ + i, ERROR[i+1] );
        }
        
        MONITORING__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( MONITORING__DOLLAR____AnalogSerialOutput__, this );
        m_StringOutputList.Add( MONITORING__DOLLAR____AnalogSerialOutput__, MONITORING__DOLLAR__ );
        
        INTEGRATION_ID_GROUP__DOLLAR__ = new InOutArray<StringOutput>( 10, this );
        for( uint i = 0; i < 10; i++ )
        {
            INTEGRATION_ID_GROUP__DOLLAR__[i+1] = new Crestron.Logos.SplusObjects.StringOutput( INTEGRATION_ID_GROUP__DOLLAR____AnalogSerialOutput__ + i, this );
            m_StringOutputList.Add( INTEGRATION_ID_GROUP__DOLLAR____AnalogSerialOutput__ + i, INTEGRATION_ID_GROUP__DOLLAR__[i+1] );
        }
        
        FROM_CORE_MODULE__DOLLAR__ = new Crestron.Logos.SplusObjects.BufferInput( FROM_CORE_MODULE__DOLLAR____AnalogSerialInput__, 10000, this );
        m_StringInputList.Add( FROM_CORE_MODULE__DOLLAR____AnalogSerialInput__, FROM_CORE_MODULE__DOLLAR__ );
        
        
        FROM_CORE_MODULE__DOLLAR__.OnSerialChange.Add( new InputChangeHandlerWrapper( FROM_CORE_MODULE__DOLLAR___OnChange_0, false ) );
        
        _SplusNVRAM.PopulateCustomAttributeList( true );
        
        NVRAM = _SplusNVRAM;
        
    }
    
    public override void LogosSimplSharpInitialize()
    {
        
        
    }
    
    public UserModuleClass_LUTRON_HOMEWORKS_QS_FEEDBACK_SORTING_MODULE_R04 ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}
    
    
    
    
    const uint SHOW_TRACE_MSGS__DigitalInput__ = 0;
    const uint FROM_CORE_MODULE__DOLLAR____AnalogSerialInput__ = 0;
    const uint ERROR__DigitalOutput__ = 0;
    const uint MONITORING__DOLLAR____AnalogSerialOutput__ = 0;
    const uint INTEGRATION_ID_GROUP__DOLLAR____AnalogSerialOutput__ = 1;
    
    [SplusStructAttribute(-1, true, false)]
    public class SplusNVRAM : SplusStructureBase
    {
    
        public SplusNVRAM( SplusObject __caller__ ) : base( __caller__ ) {}
        
        [SplusStructAttribute(0, false, true)]
            public ushort PROCESSINGBUSY = 0;
            [SplusStructAttribute(1, false, true)]
            public CrestronString PARSEDMESSAGE;
            [SplusStructAttribute(2, false, true)]
            public CrestronString PARSEDTRASH;
            
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
