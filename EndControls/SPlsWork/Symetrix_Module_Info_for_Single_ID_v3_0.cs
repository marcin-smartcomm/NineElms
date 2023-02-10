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

namespace UserModule_SYMETRIX_MODULE_INFO_FOR_SINGLE_ID_V3_0
{
    public class UserModuleClass_SYMETRIX_MODULE_INFO_FOR_SINGLE_ID_V3_0 : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        
        Crestron.Logos.SplusObjects.StringInput FROM_COMMAND_PROCESSOR;
        Crestron.Logos.SplusObjects.StringOutput TO_COMMAND_PROCESSOR;
        StringParameter CONTROL_ID;
        object FROM_COMMAND_PROCESSOR_OnChange_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 68;
                if ( Functions.TestForTrue  ( ( Functions.Find( "Send Info\r" , FROM_COMMAND_PROCESSOR ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 70;
                    MakeString ( TO_COMMAND_PROCESSOR , "{0:d} Send Info 0 {1}\r", (ushort)Functions.Atoi( FROM_COMMAND_PROCESSOR ), CONTROL_ID ) ; 
                    } 
                
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    
    public override void LogosSplusInitialize()
    {
        _SplusNVRAM = new SplusNVRAM( this );
        
        FROM_COMMAND_PROCESSOR = new Crestron.Logos.SplusObjects.StringInput( FROM_COMMAND_PROCESSOR__AnalogSerialInput__, 100, this );
        m_StringInputList.Add( FROM_COMMAND_PROCESSOR__AnalogSerialInput__, FROM_COMMAND_PROCESSOR );
        
        TO_COMMAND_PROCESSOR = new Crestron.Logos.SplusObjects.StringOutput( TO_COMMAND_PROCESSOR__AnalogSerialOutput__, this );
        m_StringOutputList.Add( TO_COMMAND_PROCESSOR__AnalogSerialOutput__, TO_COMMAND_PROCESSOR );
        
        CONTROL_ID = new StringParameter( CONTROL_ID__Parameter__, this );
        m_ParameterList.Add( CONTROL_ID__Parameter__, CONTROL_ID );
        
        
        FROM_COMMAND_PROCESSOR.OnSerialChange.Add( new InputChangeHandlerWrapper( FROM_COMMAND_PROCESSOR_OnChange_0, false ) );
        
        _SplusNVRAM.PopulateCustomAttributeList( true );
        
        NVRAM = _SplusNVRAM;
        
    }
    
    public override void LogosSimplSharpInitialize()
    {
        
        
    }
    
    public UserModuleClass_SYMETRIX_MODULE_INFO_FOR_SINGLE_ID_V3_0 ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}
    
    
    
    
    const uint FROM_COMMAND_PROCESSOR__AnalogSerialInput__ = 0;
    const uint TO_COMMAND_PROCESSOR__AnalogSerialOutput__ = 0;
    const uint CONTROL_ID__Parameter__ = 10;
    
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
