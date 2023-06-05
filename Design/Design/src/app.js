var date = new Date();
var n = date.toDateString();
var time = date.toLocaleTimeString();
let mainBtnsInitialized = false;
var isMasteriPad = false;
var volSliderPressed = false;
var volLevel = 0;
let fireAlarmState = false;

let inactivityTime = function() {
    let time;
    document.addEventListener('touchstart', function()
    {
      resetTimer();
    });
    function logout() {
        if(!fireAlarmState)
        {   
            openSubpage("ScreenSaver");
            sendMessage("DisconnectEquipment");
        } 
    }
    function resetTimer() {
      clearTimeout(time);
      time = setTimeout(logout, 15000)
    }
  };
  
function UpdateTime()
{
    var date = new Date();
    var n = date.toDateString();
    var time = date.toLocaleTimeString();

    document.getElementById("TODContainer").innerHTML = n + "\n" + time;
}

function FilRoomName(roomName)
{
    document.getElementById("roomNameContainer").innerHTML = roomName;
}

function UpdateVolumeLevel(newVol)
{
    volLevel = newVol;
    if(hasBGM || hasSonos)
    {
        if (document.getElementById("volSlider") != null) 
        {
            if(!volSliderPressed)
                document.getElementById("volSlider").value = newVol;
                
            document.getElementById("volLabel").innerHTML = newVol + "%";
        }
    }
}
function UpdateMuteState(newState)
{
    if(newState == "True")
    {
        setTimeout(() => {
            document.getElementById("volMuteIcon").classList.remove("fa-volume-high")
            document.getElementById("volMuteIcon").classList.add("fa-volume-xmark")
        }, 300);
    }
    if(newState == "False")
    {
        setTimeout(() => {
            document.getElementById("volMuteIcon").classList.add("fa-volume-high")
            document.getElementById("volMuteIcon").classList.remove("fa-volume-xmark")
        }, 300);
    }
}

function SliderOrBtnVolume()
{
    if(hasBGM || hasSonos)
    {
        if(!hasSonos && (currentSource == "Sky" || currentSource == "Freeview"))
            DrawVolBtns()
        else
            DrawSlider();
    }
    else
        DrawVolBtns()
}
function DrawSlider()
{
    var volControlsContainer = document.getElementById("volControlsContainer")
    volControlsContainer.innerHTML = "";

    var volMuteBtn = document.createElement("div")
    volMuteBtn.classList.add('btn', 'centered', 'btn-card-rectangular-big', 'vol-btn')
    volMuteBtn.setAttribute("style", "height: 100%; width: 23%; margin-right: auto")
    volMuteBtn.setAttribute("id", "volMute")
    var volMuteBtnIcon = document.createElement("div")
    volMuteBtnIcon.classList.add('fa-solid', 'fa-volume-high', 'fa-3x')
    volMuteBtnIcon.setAttribute("id", "volMuteIcon")
    volMuteBtn.appendChild(volMuteBtnIcon)

    var volSlider = document.createElement("input")
    volSlider.setAttribute("type", "range")
    volSlider.setAttribute("class", "slider")
    volSlider.setAttribute("min", "0")
    volSlider.setAttribute("max", "100")
    volSlider.setAttribute("value", "0")
    volSlider.setAttribute("id", "volSlider")
    volSlider.setAttribute("step", "5")

    var volLabel = document.createElement("div")
    volLabel.setAttribute("id", "volLabel")

    volControlsContainer.appendChild(volMuteBtn)
    volControlsContainer.appendChild(volSlider)
    volControlsContainer.appendChild(volLabel)
    
    InitializeMainBtns("slider")
    sendMessage("GetVolumeLevel")
}
function DrawVolBtns()
{
    var volControlsContainer = document.getElementById("volControlsContainer")
    volControlsContainer.innerHTML = "";

    var volMuteBtn = document.createElement("div")
    volMuteBtn.classList.add('btn', 'centered', 'btn-card-rectangular-big', 'vol-btn')
    volMuteBtn.setAttribute("style", "height: 100%; width: 23%; margin-right: 20px")
    volMuteBtn.setAttribute("id", "volMute")
    var volMuteBtnIcon = document.createElement("div")
    volMuteBtnIcon.classList.add('fa-solid', 'fa-volume-high', 'fa-3x')
    volMuteBtnIcon.setAttribute("id", "volMuteIcon")
    volMuteBtn.appendChild(volMuteBtnIcon)

    var volDownBtn = document.createElement("div")
    volDownBtn.classList.add('btn', 'centered', 'btn-card-rectangular-big', 'vol-btn')
    volDownBtn.setAttribute("style", "height: 80%; width: 23%")
    volDownBtn.setAttribute("id", "volDown")
    var volDownBtnIcon = document.createElement("div")
    volDownBtnIcon.classList.add('fa-solid', 'fa-chevron-down', 'fa-2x')
    volDownBtn.appendChild(volDownBtnIcon)

    var volLabel = document.createElement("div")
    volLabel.classList.add('volume-label')
    volLabel.setAttribute("style", "margin: auto")
    volLabel.innerHTML = "TV Volume"

    var volUpBtn = document.createElement("div")
    volUpBtn.classList.add('btn', 'centered', 'btn-card-rectangular-big', 'vol-btn')
    volUpBtn.setAttribute("style", "height: 80%; width: 23%; margin-right: auto")
    volUpBtn.setAttribute("id", "volUp")
    var volUpBtnIcon = document.createElement("div")
    volUpBtnIcon.classList.add('fa-solid', 'fa-chevron-up', 'fa-2x')
    volUpBtn.appendChild(volUpBtnIcon)

    volControlsContainer.appendChild(volMuteBtn)
    volControlsContainer.appendChild(volDownBtn)
    volControlsContainer.appendChild(volLabel)
    volControlsContainer.appendChild(volUpBtn)
    
    InitializeMainBtns("btns")
}
function InitializeMainBtns(type)
{
    if(type == "slider")
    {
        document.getElementById("volSlider").addEventListener('input', function(e)
        {
            sendMessage("Volume:"+e.target.value)
        })
        document.getElementById("volSlider").addEventListener('touchstart', function(e)
        {
            volSliderPressed = true
        })
        document.getElementById("volSlider").addEventListener('touchend', function(e)
        {
            volSliderPressed = false
            UpdateVolumeLevel(volLevel)
        })
    }
    if(type == "btns")
    {
        document.getElementById("volDown").addEventListener('click', function()
        {
            sendMessage("VolumeDown");
        })
        document.getElementById("volUp").addEventListener('click', function()
        {
            sendMessage("VolumeUp");
        })
    }
    document.getElementById("volMute").addEventListener('click', function()
    {
        sendMessage("Mute");
    })
    
    if(mainBtnsInitialized)
        return

    document.getElementById("pwrBtn").addEventListener('click', function()
    {
        TogglePopUp("PowerOff")
    })

    document.getElementById("topCornerLogo").addEventListener('click', function()
    {
        if(isMasteriPad)    openSubpage("AreaSelect")
    })

    mainBtnsInitialized = true;
}

function FireAlarmStateChanged(state)
{
    if(state.includes("True"))
    {
        console.log(state);
        document.getElementById("subpageSection").innerHTML = "<img src=\"./img/FireAlarm.jpg\" width=\"100%\" height=\"100%\">"
    }
    else
        openSubpage("ScreenSaver")
}