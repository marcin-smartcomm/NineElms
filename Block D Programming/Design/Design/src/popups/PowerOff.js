function InitializePowerOffVariables()
{
    document.getElementById("pwrOffConfirm").addEventListener('click', function()
    {
        TogglePopUp("PowerOff")
        sendMessage("RoomOff")
        sendMessage("DisconnectEquipment")
        openSubpage("ScreenSaver")
    })
    document.getElementById("pwrOffCancel").addEventListener('click', function()
    {
        TogglePopUp("PowerOff")
    })
}