var hasLights, hasSonos, hasBGM, hasTV, hasHVAC, hasFireplace, TVs;

var currentLightScene;
var currentTemp, desiredTemp;
var sources, currentSource;
var firePlaceOn;

function InitializeHomeVariables()
{
    if(hasLights) 
    {
        DrawLightsCard()
        AddLightsFb(currentLightScene)
    }
    if(hasHVAC) 
    {
        DrawHVACCard()
        DisplayCurrentTemp(currentTemp)
        DisplayDesiredTemp(desiredTemp)
    }
    if(TVs.length > 0 || hasBGM)
    {
        DrawAVCard()
        AddAVSourceFb(currentSource)
    }
    if(hasFireplace) 
    {
        DrawFireplaceCard()
        AddFireplaceFb()
    }
    //roomName in crCom.js
    if(roomName.includes("External Terrace") ||
        roomName.includes("Games Room") ||
        roomName.includes("External Pool")
      )
    {
        DrawVolumeSliderCard()
    }
}
function RemoveHomeAnimation()
{
    document.getElementById("subpageSection").classList.remove("home-enter-anim")
}
function AddHomeAnimation()
{
    document.getElementById("subpageSection").classList.add("home-enter-anim")
}


function DrawLightsCard()
{
    var labelsContainer = document.getElementById("cardLabels")
    var mainCardsContainer = document.getElementById("mainCardsContainer")

    var newLabel = document.createElement("div")
    newLabel.classList.add('container-label')
    newLabel.innerHTML = "Lights"
    labelsContainer.appendChild(newLabel)

    var newMainCard = document.createElement("div")
    newMainCard.classList.add('main-card', 'centered', 'wrapped', 'shadow-big')
    newMainCard.id = "lightsCard"

    for(let i = 0; i < 4; i++)
    {
        var newBtn = document.createElement("div")
        newBtn.classList.add('btn', 'btn-card-wide', 'centered', 'shadow-small')

        if(i == 0)
        {
            newBtn.id = `ligthsBtn${i}`
            newBtn.innerHTML = `On`
        }
        if(i == 1) 
        {
            newBtn.id = `lightsOff`
            newBtn.innerHTML = "Off"
        } 
        if(i == 2)
        {
            newBtn.id = `dimUp`
            newBtn.innerHTML = `<i class="fa-solid fa-chevron-up"></i>`
            newBtn.style.width = "38%"
            newBtn.style.marginRight = "2%"
        }
        if(i == 3)
        {
            newBtn.id = `dimDown`
            newBtn.innerHTML = `<i class="fa-solid fa-chevron-down"></i>`
            newBtn.style.width = "38%"
        }

        newMainCard.appendChild(newBtn)
    }

    mainCardsContainer.appendChild(newMainCard)
    SubscribeLightEvents()
}
function SubscribeLightEvents()
{
    for(let i = 0; i < 1; i++)
    {
        document.getElementById(`ligthsBtn${i}`).addEventListener('touchend', function()
        {
            sendMessage(`SetLightScene:${i+1}`)
        })
    }
    document.getElementById(`lightsOff`).addEventListener('touchend', function()
    {
        sendMessage("SetLightScene:0")
    })

    document.getElementById("dimUp").addEventListener("touchstart", function(){
        sendMessage("SetDim:Up:On")
    })
    document.getElementById("dimUp").addEventListener("touchend", function(){
        sendMessage("SetDim:Up:Off")
    })

    document.getElementById("dimDown").addEventListener("touchstart", function(){
        sendMessage("SetDim:Down:On")
    })
    document.getElementById("dimDown").addEventListener("touchend", function(){
        sendMessage("SetDim:Down:Off")
    })
}
function UpdateCurrentLightScene(newLightScene)
{
    currentLightScene = newLightScene;
    if(currentSubpage == "Home")
    {
        ClearLightSceneFb()
        AddLightsFb(newLightScene)
    }
}
function ClearLightSceneFb()
{
    for(let i = 0; i < 1; i++)
        document.getElementById(`ligthsBtn${i}`).classList.remove('active-btn')
    document.getElementById(`lightsOff`).classList.remove('active-btn')
}
function AddLightsFb(sceneSelected)
{
    if (sceneSelected == 0)
        document.getElementById(`lightsOff`).classList.add('active-btn')
    else
        document.getElementById(`ligthsBtn${sceneSelected-1}`).classList.add('active-btn')
}



function DrawHVACCard()
{
    var labelsContainer = document.getElementById("cardLabels")
    var newMainCard = document.createElement("div")
    newMainCard.classList.add('main-card', 'centered', 'wrapped', 'shadow-big')
    newMainCard.id = "climateCard"

    var newLabel = document.createElement("div")
    newLabel.classList.add('container-label')
    newLabel.innerHTML = "Climate"
    labelsContainer.appendChild(newLabel)

    var rawFile = new XMLHttpRequest();
    rawFile.open("GET", './mainPageCards/HVACCard.html', false);
    rawFile.onreadystatechange = function ()
    {
        if(rawFile.readyState === 4)
        {
            if(rawFile.status === 200 || rawFile.status == 0)
            {
                var allText = rawFile.responseText;
                newMainCard.innerHTML = allText;
                document.getElementById("mainCardsContainer").appendChild(newMainCard)
            }
        }
    }
    rawFile.send(null);
    rawFile.DONE;

    SubscribeHVACEvents()
}
function SubscribeHVACEvents()
{
    document.getElementById("tempUpBtn").addEventListener('touchend', function()
    {
        sendMessage("TempUp")
    })
    document.getElementById("tempDownBtn").addEventListener('touchend', function()
    {
        sendMessage("TempDown")
    })
}
function UpdateCurrentTemp(newTemp)
{
    currentTemp = newTemp
    if(currentSubpage == "Home")
        DisplayCurrentTemp(newTemp)
}
function UpdateDesiredTemp(newTemp)
{
    desiredTemp = newTemp
    if(currentSubpage == "Home")
        DisplayDesiredTemp(newTemp)
}
function DisplayDesiredTemp(temp)
{
    if(temp.includes("."))
        document.getElementById("desiredTempLabel").innerHTML = temp + "°C"
    else
        document.getElementById("desiredTempLabel").innerHTML = temp + ".0°C"
}
function DisplayCurrentTemp(temp)
{
    if(temp.includes("."))
        document.getElementById("actualTempLabel").innerHTML = temp + "°C"
    else
        document.getElementById("actualTempLabel").innerHTML = temp + ".0°C"
}



function DrawAVCard()
{
    var labelsContainer = document.getElementById("cardLabels")
    var newMainCard = document.createElement("div")
    newMainCard.classList.add('main-card', 'centered', 'wrapped', 'shadow-big')
    newMainCard.id = "avCard"

    var newLabel = document.createElement("div")
    newLabel.classList.add('container-label')
    newLabel.innerHTML = "AV"
    labelsContainer.appendChild(newLabel)

    for(var i = 0; i < sources.length; i++)
    {
        var newBtn = document.createElement("div")
        newBtn.classList.add('btn', 'btn-card-wide', 'centered', 'shadow-small', 'wrapped')
        newBtn.id = `srcBtn${i}`
        newBtn.innerHTML = `<div>${sources[i]}</div>`;
        newMainCard.appendChild(newBtn)
    }

    mainCardsContainer.appendChild(newMainCard)
    SubscribeAVEvents()
}
function SubscribeAVEvents()
{
    for(let i = 0; i < sources.length; i++)
    {
        document.getElementById(`srcBtn${i}`).addEventListener('click', function()
        {
            if(TVs.length > 1 && (sources[i] == "Sky" || sources[i] == "Freeview"))
            {
                ClearAVBtnsFb();
                ExpandSourceBtn(`srcBtn${i}`);
            }
            else 
            {
                sendMessage(`SetSourceSelected:${sources[i]}`)
                if(sources[i] == "Sky" || sources[i] == "Freeview")
                {
                    openSubpage(sources[i])
                    if(!hasSonos && !roomName.includes("Games Room") && !roomName.includes("Yoga and Spin") && !roomName.includes("Bar / Lounge"))
                      DrawVolBtns()
                }
                else
                {
                    if(hasBGM)
                      DrawSlider()
                }
            }
        })
    }
}
function ExpandSourceBtn(btn)
{
    if(currentSubpage == "Home")

    mainSourceBtnRef = document.getElementById(btn);

    if(!mainSourceBtnRef.classList.contains("expand"))
    {
        for(let i = 0; i < sources.length; i++)
        {
            if(document.getElementById(`srcBtn${i}`).classList.contains("expand"))
            {
                document.getElementById(`srcBtn${i}`).classList.remove("expand")
                document.getElementById(`srcBtn${i}`).classList.add("shrink")
                document.getElementById(`srcBtn${i}`).classList.add("btn")
                document.getElementById(`srcBtn${i}`).innerHTML = `<div style="width: 100%; text-align: center;">${sources[i]}</div>`
            }
        }
        mainSourceBtnRef.classList.remove("shrink")
        mainSourceBtnRef.classList.remove("btn")
        mainSourceBtnRef.classList.add("expand")
        mainSourceBtnRef.firstChild.style.width = "100%";
    
        var sourceName = mainSourceBtnRef.childNodes[0].innerHTML;
        for(let i = 0; i < TVs.length; i++)
        {
            var tvBtn = document.createElement("div")
            tvBtn.classList.add(`btn`, `btn-card-rectangular-special`, `centered`)
            tvBtn.id = TVs[i]+":"+sourceName;
            tvBtn.innerHTML = TVs[i];
            mainSourceBtnRef.appendChild(tvBtn)
        }
        for(let i = 0; i < TVs.length; i++)
        {
            var btn = document.getElementById(TVs[i]+":"+sourceName)
            btn.addEventListener('touchend', function(){
                sendMessage(TVs[i]+":"+sourceName)
                sendMessage("GetSourceSelected")
            })
        }

        var sourceControlBtn = document.createElement("div")
        sourceControlBtn.classList.add('btn', 'btn-card-wide-special', 'centered')
        sourceControlBtn.id = `${sourceName}`
        sourceControlBtn.innerHTML = "Source Control"
        mainSourceBtnRef.appendChild(sourceControlBtn)

        document.getElementById(sourceControlBtn.id).addEventListener('touchend', function()
        {
            openSubpage(sourceControlBtn.id);
        })
    }
}
function ClearAVBtnsFb()
{
    if(currentSubpage != "Home")
        return;

    for(let i = 0; i < sources.length; i++)
    {
        document.getElementById(`srcBtn${i}`).classList.remove("active-btn")
    }
}
function AddAVSourceFb(source)
{
    for(let i = 0; i < sources.length; i++)
    {
        if(sources[i] == source)
            document.getElementById(`srcBtn${i}`).classList.add("active-btn")
    }
}
function ProcessSourceChangedEvent(newSource)
{
    currentSource = newSource;
    if(newSource == "Off")
    {
        UpdateMuteState("True")
        UpdateVolumeLevel("0")
        if(hasBGM || hasSonos)
            if(!hasSonos && (currentSource == "Sky" || currentSource == "Freeview"))
                document.getElementById("volLabel").innerHTML = "";
    }
    else
    {
        if(currentSubpage == "Home")
        {
            ClearAVBtnsFb()
            AddAVSourceFb(newSource)
        }
    }
}



function DrawFireplaceCard()
{
    var labelsContainer = document.getElementById("cardLabels")
    var newMainCard = document.createElement("div")
    newMainCard.classList.add('main-card', 'centered', 'wrapped', 'shadow-big')
    newMainCard.id = "fireplaceCard"

    var newLabel = document.createElement("div")
    newLabel.classList.add('container-label')
    newLabel.innerHTML = "Fireplace"
    labelsContainer.appendChild(newLabel)

    var newBtn = document.createElement("div")
    newBtn.classList.add('btn', 'btn-card-wide', 'centered', 'shadow-small', 'wrapped')
    newBtn.id = `fireplaceOnBtn`
    newBtn.innerHTML = `Fireplace On`;
    newMainCard.appendChild(newBtn)

    var newBtn = document.createElement("div")
    newBtn.classList.add('btn', 'btn-card-wide', 'centered', 'shadow-small', 'wrapped')
    newBtn.id = `fireplaceOffBtn`
    newBtn.innerHTML = `Fireplace Off`;
    newMainCard.appendChild(newBtn)

    mainCardsContainer.appendChild(newMainCard)
    SubscribeFireplaceEvents()
}
function SubscribeFireplaceEvents()
{
    document.getElementById("fireplaceOnBtn").addEventListener('touchend', function()
    {
        sendMessage("SetFireplace:true")
    })
    document.getElementById("fireplaceOffBtn").addEventListener('touchend', function()
    {
        sendMessage("SetFireplace:false")
    })
}
function AddFireplaceFb()
{
    if(firePlaceOn)
    {
        document.getElementById("fireplaceOnBtn").classList.add('active-btn')
        document.getElementById("fireplaceOffBtn").classList.remove('active-btn')
    }
    else
    {
        document.getElementById("fireplaceOffBtn").classList.add('active-btn')
        document.getElementById("fireplaceOnBtn").classList.remove('active-btn')
    }
}
function ClearFireplaceBtnsFb()
{
    document.getElementById("fireplaceOnBtn").classList.remove('active-btn')
    document.getElementById("fireplaceOffBtn").classList.remove('active-btn')
}
function FireplaceStateChanged(value)
{
    if (value == "True")
        firePlaceOn = true;
    if(value == "False")
        firePlaceOn = false;

    if(currentSubpage == "Home")
        AddFireplaceFb()
}



function DrawVolumeSliderCard()
{
    var labelsContainer = document.getElementById("cardLabels")
    var newMainCard = document.createElement("div")
    newMainCard.classList.add('main-card', 'centered', 'wrapped', 'shadow-big')
    newMainCard.id = "individualVolumeCard"

    var newLabel = document.createElement("div")
    newLabel.classList.add('container-label')
    newLabel.innerHTML = "Zones Control"
    labelsContainer.appendChild(newLabel)

    var rawFile = new XMLHttpRequest();

    //roomName in crCom.js
    if(roomName.includes("External Terrace") && processorID == 3)
        rawFile.open("GET", './mainPageCards/TerraceVolumeSliders.html', false);
    if(roomName.includes("External Terrace") && processorID == 5)
        rawFile.open("GET", './mainPageCards/BlockBTerraceVolumeSliders.html', false);
    if(roomName.includes("Games Room"))
        rawFile.open("GET", './mainPageCards/GamesVolumeSliders.html', false);
    if(roomName.includes("External Pool"))
        rawFile.open("GET", './mainPageCards/PoolVolumeSliders.html', false);

    rawFile.onreadystatechange = function ()
    {
        if(rawFile.readyState === 4)
        {
            if(rawFile.status === 200 || rawFile.status == 0)
            {
                var allText = rawFile.responseText;
                newMainCard.innerHTML = allText;
                document.getElementById("mainCardsContainer").appendChild(newMainCard)
            }
        }
    }
    rawFile.send(null);
    rawFile.DONE;

    SubscribeVolSlidersEvents()
    sendMessage("GetIndividualVolumes")
    sendMessage("GetIndividualMutes")
}
function SubscribeVolSlidersEvents()
{
    document.getElementById("zone1VolSlider").addEventListener('input', function(e)
    {
        sendMessage("IndividualVolume:1:"+e.target.value)
    })
    document.getElementById("zone2VolSlider").addEventListener('input', function(e)
    {
        sendMessage("IndividualVolume:2:"+e.target.value)
    })

    if(roomName.includes("External Terrace"))
    {
        if(processorID == 3 || processorID == 5)
        {
            document.getElementById("zone3VolSlider").addEventListener('input', function(e)
            {
                sendMessage("IndividualVolume:3:"+e.target.value)
            })
        }
        if(processorID == '3')
        {
            document.getElementById("zone4VolSlider").addEventListener('input', function(e)
            {
                sendMessage("IndividualVolume:4:"+e.target.value)
            })
        }
    }

    document.getElementById("zone1VolMute").addEventListener('touchend', function()
    {
        sendMessage("IndividualMute:1")
    })
    document.getElementById("zone2VolMute").addEventListener('touchend', function()
    {
        sendMessage("IndividualMute:2")
    })

    if(roomName.includes("External Terrace"))
    {
        if(processorID == '3' || processorID == '5')
        {
            document.getElementById("zone3VolMute").addEventListener('touchend', function()
            {
                sendMessage("IndividualMute:3")
            })
        }
        if(processorID == '3')
        {
            document.getElementById("zone4VolMute").addEventListener('touchend', function()
            {
                sendMessage("IndividualMute:4")
            })
        }
    }
}
function AddZoneSlidersFb(zoneNum, newLevel)
{
    if(currentSubpage == "Home")
        document.getElementById(`zone${zoneNum}VolSlider`).value = newLevel;
}
function AddZoneMuteStates(zoneNum, newState)
{
    if(currentSubpage != "Home")
        return;

    if(newState == "True")
    {
        document.getElementById(`zone${zoneNum}VolMuteIcon`).classList.remove("fa-volume-high")
        document.getElementById(`zone${zoneNum}VolMuteIcon`).classList.add("fa-volume-xmark")
    }
    else
    {
        document.getElementById(`zone${zoneNum}VolMuteIcon`).classList.add("fa-volume-high")
        document.getElementById(`zone${zoneNum}VolMuteIcon`).classList.remove("fa-volume-xmark")
    }
}