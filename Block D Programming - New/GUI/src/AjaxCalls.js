function AjaxGETCall(endpoint, params)
{
    let dataToSend = ''

    if(params.length === 1) dataToSend = params[0];
    else if (params.length > 1)
    {
        $.each(params, function (i, value) { 
            if(i === params.length-1) dataToSend += value
            else dataToSend += value+':'
        });
    }

    return $.get(`http://${coreProcessorIP}:50000/api/${endpoint}?${dataToSend}`).responseJSON
}

function StartUpdateCalls()
{
    updateInterval = window.setInterval(GetRoomUpdateCall, 5000)
}

function GetRoomUpdateCall()
{
    response = AjaxGETCall("RoomInfoUpdate", [roomCoreData.roomID])
    ProcessUpdateResponse(response)
}

function ProcessUpdateResponse(response)
{
    if(response.fireAlarm) OpenPopUp("FireAlarm")
    if(!response.fireAlarm) ClosePopUp("FireAlarm")

    UpdateSelectedSource(response.sourceSelected)

    if($('#volSlider').length > 0) UpdateVolControls(response.volLevel)
    UpdateMuteState(response.volMute)
}