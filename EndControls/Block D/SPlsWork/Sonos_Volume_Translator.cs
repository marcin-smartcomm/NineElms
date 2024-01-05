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

namespace UserModule_SONOS_VOLUME_TRANSLATOR
{
    public class UserModuleClass_SONOS_VOLUME_TRANSLATOR : SplusObject
    {
        static CCriticalSection g_criticalSection = new CCriticalSection();
        
        
        StringParameter PLAYERNAME;
        Crestron.Logos.SplusObjects.StringInput RX__DOLLAR__;
        Crestron.Logos.SplusObjects.AnalogInput VOLLEVEL_FB__POUND__;
        Crestron.Logos.SplusObjects.StringOutput TX__DOLLAR__;
        Crestron.Logos.SplusObjects.StringOutput PLAYERNAME__DOLLAR__;
        Crestron.Logos.SplusObjects.AnalogOutput VOLLEVEL__POUND__;
        object RX__DOLLAR___OnChange_0 ( Object __EventInfo__ )
        
            { 
            Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
            try
            {
                SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
                
                __context__.SourceCodeLine = 16;
                if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Left( RX__DOLLAR__ , (int)( 6 ) ) == PLAYERNAME ))  ) ) 
                    { 
                    __context__.SourceCodeLine = 18;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Mid( RX__DOLLAR__ , (int)( 8 ) , (int)( 6 ) ) == "Volume"))  ) ) 
                        { 
                        __context__.SourceCodeLine = 20;
                        VOLLEVEL__POUND__  .Value = (ushort) ( Functions.Atoi( Functions.Mid( RX__DOLLAR__ , (int)( 15 ) , (int)( 3 ) ) ) ) ; 
                        } 
                    
                    __context__.SourceCodeLine = 22;
                    if ( Functions.TestForTrue  ( ( Functions.BoolToInt (Functions.Mid( RX__DOLLAR__ , (int)( 8 ) , (int)( 4 ) ) == "Name"))  ) ) 
                        { 
                        __context__.SourceCodeLine = 24;
                        PLAYERNAME__DOLLAR__  .UpdateValue ( Functions.Mid ( RX__DOLLAR__ ,  (int) ( 13 ) ,  (int) ( 20 ) )  ) ; 
                        } 
                    
                    } 
                
                
                
            }
            catch(Exception e) { ObjectCatchHandler(e); }
            finally { ObjectFinallyHandler( __SignalEventArg__ ); }
            return this;
            
        }
        
    object VOLLEVEL_FB__POUND___OnChange_1 ( Object __EventInfo__ )
    
        { 
        Crestron.Logos.SplusObjects.SignalEventArgs __SignalEventArg__ = (Crestron.Logos.SplusObjects.SignalEventArgs)__EventInfo__;
        try
        {
            SplusExecutionContext __context__ = SplusThreadStartCode(__SignalEventArg__);
            
            __context__.SourceCodeLine = 31;
            TX__DOLLAR__  .UpdateValue ( PLAYERNAME + ":Volume:" + Functions.ItoA (  (int) ( VOLLEVEL_FB__POUND__  .UshortValue ) )  ) ; 
            
            
        }
        catch(Exception e) { ObjectCatchHandler(e); }
        finally { ObjectFinallyHandler( __SignalEventArg__ ); }
        return this;
        
    }
    

public override void LogosSplusInitialize()
{
    _SplusNVRAM = new SplusNVRAM( this );
    
    VOLLEVEL_FB__POUND__ = new Crestron.Logos.SplusObjects.AnalogInput( VOLLEVEL_FB__POUND____AnalogSerialInput__, this );
    m_AnalogInputList.Add( VOLLEVEL_FB__POUND____AnalogSerialInput__, VOLLEVEL_FB__POUND__ );
    
    VOLLEVEL__POUND__ = new Crestron.Logos.SplusObjects.AnalogOutput( VOLLEVEL__POUND____AnalogSerialOutput__, this );
    m_AnalogOutputList.Add( VOLLEVEL__POUND____AnalogSerialOutput__, VOLLEVEL__POUND__ );
    
    RX__DOLLAR__ = new Crestron.Logos.SplusObjects.StringInput( RX__DOLLAR____AnalogSerialInput__, 100, this );
    m_StringInputList.Add( RX__DOLLAR____AnalogSerialInput__, RX__DOLLAR__ );
    
    TX__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( TX__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( TX__DOLLAR____AnalogSerialOutput__, TX__DOLLAR__ );
    
    PLAYERNAME__DOLLAR__ = new Crestron.Logos.SplusObjects.StringOutput( PLAYERNAME__DOLLAR____AnalogSerialOutput__, this );
    m_StringOutputList.Add( PLAYERNAME__DOLLAR____AnalogSerialOutput__, PLAYERNAME__DOLLAR__ );
    
    PLAYERNAME = new StringParameter( PLAYERNAME__Parameter__, this );
    m_ParameterList.Add( PLAYERNAME__Parameter__, PLAYERNAME );
    
    
    RX__DOLLAR__.OnSerialChange.Add( new InputChangeHandlerWrapper( RX__DOLLAR___OnChange_0, false ) );
    VOLLEVEL_FB__POUND__.OnAnalogChange.Add( new InputChangeHandlerWrapper( VOLLEVEL_FB__POUND___OnChange_1, false ) );
    
    _SplusNVRAM.PopulateCustomAttributeList( true );
    
    NVRAM = _SplusNVRAM;
    
}

public override void LogosSimplSharpInitialize()
{
    
    
}

public UserModuleClass_SONOS_VOLUME_TRANSLATOR ( string InstanceName, string ReferenceID, Crestron.Logos.SplusObjects.CrestronStringEncoding nEncodingType ) : base( InstanceName, ReferenceID, nEncodingType ) {}




const uint PLAYERNAME__Parameter__ = 10;
const uint RX__DOLLAR____AnalogSerialInput__ = 0;
const uint VOLLEVEL_FB__POUND____AnalogSerialInput__ = 1;
const uint TX__DOLLAR____AnalogSerialOutput__ = 0;
const uint PLAYERNAME__DOLLAR____AnalogSerialOutput__ = 1;
const uint VOLLEVEL__POUND____AnalogSerialOutput__ = 2;

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
