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

namespace UserModule_SNS_COMMS
{
    public class UserModuleClass_SNS_COMMS : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        
        
        StringParameter ROOMIDENTIFIER;
        Crestron.Logos.SplusObjects.AnalogInput VOL_FB__POUND__;
        Crestron.Logos.SplusObjects.StringInput RX__DOLLAR__;
        Crestron.Logos.SplusObjects.AnalogOutput VOLUME__POUND__;
        Crestron.Logos.SplusObjects.StringOutput TX__DOLLAR__;
        object RX__DOLLAR___OnChange_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 23;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Left( RX__DOLLAR__ , (int)( 3 ) ) == "SNS"))  ) ) 
                    { 
                    __context__.SourceCodeLine = 25;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt ( Functions.Find( ROOMIDENTIFIER  , RX__DOLLAR__ ) > 0 ))  ) ) 
                        { 
                        __context__.SourceCodeLine = 27;
                        if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Mid( RX__DOLLAR__ , (int)( 12 ) , (int)( 6 ) ) == "Volume"))  ) ) 
                            { 
                            __context__.SourceCodeLine = 29;
                            VOLUME__POUND__  .Value = (ushort) ( Functions.Atoi( Functions.Mid( RX__DOLLAR__ , (int)( 19 ) , (int)( 3 ) ) ) ) ; 
                            } 
                        
                        } 
                    
                    } 
                
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object VOL_FB__POUND___OnChange_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 37;
            TX__DOLLAR__  .UpdateValue ( "BGM:" + ROOMIDENTIFIER + ":Volume:" + Functions.ItoA (  (int) ( VOL_FB__POUND__  .UshortValue ) )  ) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    

public override void LogosSplusInitialize()
{
    _SplusNVRAM = new SplusNVRAM( this );
    
    VOL_FB__POUND__ = new Crestron.Logos.SplusObjects.AnalogInput( VOL_FB__POUND____AnalogSerialInput__, this );
    m_AnalogInputList.Add( VOL_FB__POUND____AnalogSerialInput__, VOL_FB__POUND__ );
    
    VOLUME__POUND__ = new Crestron.Logos.SplusObjects.AnalogOutput( VOLUME__POUND____AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( VOLUME__POUND____AnalogSerialOutput__, VOLUME__POUND__ );
    
    RX__DOLLAR__ = new Crestron.Logos.SplusObjects.StringInput( RX__DOLLAR____AnalogSerialInput__, 100, this );
    m_StringInputList.Add( RX__DOLLAR____AnalogSerialInput__, RX__DOLLAR__ );
    
    TX__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( TX__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( TX__DOLLAR____AnalogSerialOutput__, TX__DOLLAR__ );
    
    ROOMIDENTIFIER = new StringParameter( ROOMIDENTIFIER__Parameter__, this );
    m_ParameterList.Add( ROOMIDENTIFIER__Parameter__, ROOMIDENTIFIER );
    
    
    RX__DOLLAR__.OnSerialChange.Add( new InputChangeHandlerWrapper( RX__DOLLAR___OnChange_0, false ) );
    VOL_FB__POUND__.OnAnalogChange.Add( new InputChangeHandlerWrapper( VOL_FB__POUND___OnChange_1, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_SNS_COMMS ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint ROOMIDENTIFIER__Parameter__ = 10;
const uint VOL_FB__POUND____AnalogSerialInput__ = 0;
const uint RX__DOLLAR____AnalogSerialInput__ = 1;
const uint VOLUME__POUND____AnalogSerialOutput__ = 0;
const uint TX__DOLLAR____AnalogSerialOutput__ = 1;

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
