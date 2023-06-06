let _webSocket
let _WebSocketAddress

function RequestRoomData()
{
    sendMessage("GetRoomName")
    sendMessage("GetSourceSelected")
    sendMessage("GetSources")

    sendMessage("hasSonos")
    sendMessage("hasBGM")
    sendMessage("hasLights")
    sendMessage("hasHVAC")
    sendMessage("hasFireplace")
    sendMessage("hasTV")
    
    ///*----------------Comment Fire Alarm state out only for iPad-----------------
    setTimeout(() => {
        sendMessage("GetFireAlarmState")
    }, 1000);
    //----------------------------------------------------------------------------*/
}

/*-------------------Connection Settings for iPad-------------------------
_WebSocketAddress = localStorage.getItem("address")
if(_WebSocketAddress == undefined)
{
    _webSocket = new WebSocket('ws://172.16.98.100:50100')
    //_webSocket = new WebSocket('ws://192.168.1.243:50100')
}
else   
    //_webSocket = new WebSocket('ws://192.168.1.243:50100')
    //_webSocket = new WebSocket('ws://172.16.98.100:50100')
    _webSocket = new WebSocket(_WebSocketAddress)

//----------------------------------------------------------------------------*/

///*----------------------Connection Settings for TSW---------------------------
    _webSocket = new WebSocket('ws://172.16.98.101:50000')
    //_webSocket = new WebSocket('ws://192.168.1.243:50004')
//----------------------------------------------------------------------------*/

var interval;
let roomName

document.onload = inactivityTime();

_webSocket.onmessage = function(e) {
    onMessage(e);
}

_webSocket.onopen = function(e) {
    ping();
    setInterval(ping, 10000);
    socketConnected = true;

    RequestRoomData();

    //if connected time should be counting
    interval = window.setInterval(UpdateTime, 1000);
}

_webSocket.onerror = function(e)
{
    console.log("error connecting");
    setTimeout(() => {
        location.reload();
    }, 1000);
}

function sendMessage(message)
{
    _webSocket.send("STRING[1,"+message+"]");
}

let socketConnected = false;
async function ping() {   
    if (_webSocket.readyState === 0 || _webSocket.readyState === 3)
    {
        socketConnected = false;
        location.reload();
    }
    
    if(socketConnected)
    {
        _webSocket.send('STRING[1,__ping__]');
    }

    tm = setTimeout(function () {
        window.clearInterval(interval)
        connStatus('controlSystemStatus', 'red', 'Error');
    }, 3000);
}

function connStatus(elementID, color, message)
{
    if(currentSubpage != "ScreenSaver")
    {
        
    }
}

function pong() {
    interval = window.setInterval(UpdateTime, 1000);
    connStatus('controlSystemStatus', 'green', 'Connected');
    //sendMessage("FA:true");
    clearTimeout(tm);
}

let neighbourRoom = "";

function onMessage(e) {
  const msg = e.data;
  const value = getBoundString_EndLastIndex(msg, ",", "]"); 
  console.log(e.data);
    if (value == '__pong__') {
        pong();
        return;
    }
    else if (value.includes("Sonos"))
    {
        //hasSonos in Home.js
        if (value.includes("True"))
            hasSonos = true;
        if(value.includes("False"))
            hasSonos = false;
    }
    else if (value.includes("BGM"))
    {
        //hasBGM in Home.js
        if (value.includes("True"))
            hasBGM = true;
        if(value.includes("False"))
            hasBGM = false;

        //in app.js
        SliderOrBtnVolume();
        sendMessage("GetVolumeLevel");
        sendMessage("GetMuteState");
    }
    else if(value.includes("Lights"))
    {
        //hasLights in Home.js
        if (value.includes("True"))
        {
            hasLights = true;
        }
        if(value.includes("False"))
            hasLights = false;
    }
    else if(value.includes("HVAC"))
    {
        //hasHVAC in Home.js
        if (value.includes("True"))
            hasHVAC = true;
        if(value.includes("False"))
            hasHVAC = false;
    }
    else if(value.includes("FireplaceState"))
    {
        //in Home.js
        var fireplaceState = value.replace('FireplaceState ', '')
        FireplaceStateChanged(fireplaceState)
    }
    else if(value.includes("Fireplace"))
    {
        //hasFireplace in Home.js
        if (value.includes("True"))
            hasFireplace = true;
        if(value.includes("False"))
            hasFireplace = false;
    }
    else if(value.includes("TVs"))
    {
        //TVs in Home.js
        if(value.includes("null")) { TVs = [] }
        else
        {
            var roomSetupInfo = value.replace('TVs ', '')
            TVs = roomSetupInfo.split(':')
        }
    }
    else if(value.includes("RoomName"))
    {
        roomName = value.replace('RoomName ', '');
        //in app.js
        FilRoomName(roomName);
    }
    else if(value.includes("Sources"))
    {
        let roomSetupInfo = value.replace('Sources ', '')
        
        //in Home.js
        sources = roomSetupInfo.split(':')
    }
    else if(value.includes("SourceSelected"))
    {
        let sourceSelected = value.replace('SourceSelected ', '');

        //in app.js
        ProcessSourceChangedEvent(sourceSelected);
    }
    else if(value.includes("TV Connected"))
    {
        connStatus('tvStatus', 'green', 'Connected'); 
        tvConnStatus = "Connected";
    }
    else if(value.includes("TV Disconnected"))
    {
        connStatus('tvStatus', 'red', 'Error'); 
        tvConnStatus = "Error";
    }
    else if(value.includes("ZoneVolume"))
    {
        //in Home.js
        var newInfo = value.replace('ZoneVolume ', '').split(':');
        AddZoneSlidersFb(parseInt(newInfo[0]), parseInt(newInfo[1]));
    }
    else if(value.includes("Volume"))
    {
        let temp = value.replace('Volume ', '');

        //in app.js
        UpdateVolumeLevel(temp);
    }
    else if(value.includes("ZoneMuteState"))
    {
        //in Home.js
        var newInfo = value.replace('ZoneMuteState ', '').split(':');
        AddZoneMuteStates(parseInt(newInfo[0]), newInfo[1]);
    }
    else if(value.includes("MuteState"))
    {
        let temp = value.replace('MuteState ', '');

        //in app.js
        UpdateMuteState(temp);
    }
    else if(value.includes("LightScene"))
    {
        //in Home.js
        temp = value.replace('LightScene ', '');
        UpdateCurrentLightScene(temp);
    }
    else if(value.includes("ActualTemp"))
    {
        //in Home.js
        temp = value.replace('ActualTemp ', '');
        UpdateCurrentTemp(temp);
    }
    else if(value.includes("DesiredTemp"))
    {
        //in Home.js
        temp = value.replace('DesiredTemp ', '');
        UpdateDesiredTemp(temp);
    }

    //Roaming masteriPad comms
    else if(value.includes("MasteriPad"))
    {
        if(value.includes("True"))
        {
            isMasteriPad = true;
            openSubpage("AreaSelect")
            
            document.getElementById("roomNameContainer").addEventListener('click', function() {
                openSubpage("Home")
            })
        }
    }
    else if(value.includes("ProcessorID"))
    {
        var temp = value.replace('ProcessorID ', '')

        //in AreaSelect.js
        UpdateProcessorSelected(temp)
    }
    else if(value.includes("RoomsList"))
    {
        var temp = value.replace('RoomsList ', '')
        temp = temp.split(':')

        //in AreaSelect.js
        UpdateAvailableRoomsList(temp)
    }
    else if(value.includes("RoomChanged"))
    {
        RequestRoomData()
    }
    else if (value.includes("FireAlarm"))
    {
        var temp = value.replace('FireAlarm ', '')

        //app.js
        if(temp.includes("True"))
            fireAlarmState = true;
        else
            fireAlarmState = false;
        
        FireAlarmStateChanged(temp);
    }
}
 
function getBoundString_EndLastIndex(msg, startChar, stopChar)
{
    let response = "";
         
    if (msg != null && msg.length > 0)
    {
        let start = msg.indexOf(startChar);
             
        if (start >= 0)
        {
            start += startChar.length;
                 
            let end = msg.lastIndexOf(stopChar);
             
            if (start < end)
            {
                response = msg.substring(start, end);
            }
        }
    }
         
    return response;
}