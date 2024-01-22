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

namespace UserModule_NINEELMSHVACTEMPTRANSLATOR
{
    public class UserModuleClass_NINEELMSHVACTEMPTRANSLATOR : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        StringParameter PROCESSOR;
        StringParameter ROOM;
        Crestron.Logos.SplusObjects.AnalogInput ACTUAL_TEMP__POUND__;
        Crestron.Logos.SplusObjects.AnalogInput DESIRED_TEMP__POUND__;
        Crestron.Logos.SplusObjects.StringOutput ACTUAL_TEMP__DOLLAR__;
        Crestron.Logos.SplusObjects.StringOutput DESIRED_TEMP__DOLLAR__;
        object ACTUAL_TEMP__POUND___OnChange_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                CrestronString NEWTEMP__DOLLAR__;
                NEWTEMP__DOLLAR__  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 10, this );
                
                
                __context__.SourceCodeLine = 14;
                NEWTEMP__DOLLAR__  .UpdateValue ( Functions.ItoA (  (int) ( ACTUAL_TEMP__POUND__  .UshortValue ) )  ) ; 
                __context__.SourceCodeLine = 16;
                ACTUAL_TEMP__DOLLAR__  .UpdateValue ( "HVAC:" + PROCESSOR + ":" + ROOM + ":ActualTemp:" + Functions.Left ( NEWTEMP__DOLLAR__ ,  (int) ( 2 ) ) + "." + Functions.Right ( NEWTEMP__DOLLAR__ ,  (int) ( 1 ) )  ) ; 
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object DESIRED_TEMP__POUND___OnChange_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            CrestronString NEWTEMP__DOLLAR__;
            NEWTEMP__DOLLAR__  = new CrestronString( Crestron.Logos.SplusObjects.CrestronStringEncoding.eEncodingASCII, 10, this );
            
            
            __context__.SourceCodeLine = 22;
            NEWTEMP__DOLLAR__  .UpdateValue ( Functions.ItoA (  (int) ( DESIRED_TEMP__POUND__  .UshortValue ) )  ) ; 
            __context__.SourceCodeLine = 24;
            DESIRED_TEMP__DOLLAR__  .UpdateValue ( "HVAC:" + PROCESSOR + ":" + ROOM + ":DesiredTemp:" + Functions.Left ( NEWTEMP__DOLLAR__ ,  (int) ( 2 ) ) + "." + Functions.Right ( NEWTEMP__DOLLAR__ ,  (int) ( 1 ) )  ) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    

public override void LogosSplusInitialize()
{
    _SplusNVRAM = new SplusNVRAM( this );
    
    ACTUAL_TEMP__POUND__ = new Crestron.Logos.SplusObjects.AnalogInput( ACTUAL_TEMP__POUND____AnalogSerialInput__, this );
    m_AnalogInputList.Add( ACTUAL_TEMP__POUND____AnalogSerialInput__, ACTUAL_TEMP__POUND__ );
    
    DESIRED_TEMP__POUND__ = new Crestron.Logos.SplusObjects.AnalogInput( DESIRED_TEMP__POUND____AnalogSerialInput__, this );
    m_AnalogInputList.Add( DESIRED_TEMP__POUND____AnalogSerialInput__, DESIRED_TEMP__POUND__ );
    
    ACTUAL_TEMP__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( ACTUAL_TEMP__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( ACTUAL_TEMP__DOLLAR____AnalogSerialOutput__, ACTUAL_TEMP__DOLLAR__ );
    
    DESIRED_TEMP__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( DESIRED_TEMP__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( DESIRED_TEMP__DOLLAR____AnalogSerialOutput__, DESIRED_TEMP__DOLLAR__ );
    
    PROCESSOR = new StringParameter( PROCESSOR__Parameter__, this );
    m_ParameterList.Add( PROCESSOR__Parameter__, PROCESSOR );
    
    ROOM = new StringParameter( ROOM__Parameter__, this );
    m_ParameterList.Add( ROOM__Parameter__, ROOM );
    
    
    ACTUAL_TEMP__POUND__.OnAnalogChange.Add( new InputChangeHandlerWrapper( ACTUAL_TEMP__POUND___OnChange_0, false ) );
    DESIRED_TEMP__POUND__.OnAnalogChange.Add( new InputChangeHandlerWrapper( DESIRED_TEMP__POUND___OnChange_1, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_NINEELMSHVACTEMPTRANSLATOR ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint PROCESSOR__Parameter__ = 10;
const uint ROOM__Parameter__ = 11;
const uint ACTUAL_TEMP__POUND____AnalogSerialInput__ = 0;
const uint DESIRED_TEMP__POUND____AnalogSerialInput__ = 1;
const uint ACTUAL_TEMP__DOLLAR____AnalogSerialOutput__ = 0;
const uint DESIRED_TEMP__DOLLAR____AnalogSerialOutput__ = 1;

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
