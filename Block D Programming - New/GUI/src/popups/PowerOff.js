function InitializePowerOffVariables()
{
    document.getElementById("pwrOffConfirm").addEventListener('click', function()
    {
        responseJSON = AjaxGETCall("RoomShutdown", [roomCoreData.roomID])
        roomCoreData.sourceSelected = responseJSON.currentSource
        UpdateSelectedSource()
        
        openSubpage("ScreenSaver")
        ClosePopUp("PowerOff")
        $('#volControlsContainer').html("")
    })
    document.getElementById("pwrOffCancel").addEventListener('click', function()
    {
        ClosePopUp("PowerOff")
    })
}