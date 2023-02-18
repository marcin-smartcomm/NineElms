function InitializeAreaSelectVariables()
{
    sendMessage("GetProcessorID")
    sendMessage("GetRoomsList")
}

function UpdateProcessorSelected(procID)
{
    ClearFloorsFb()
    document.getElementById(`proc${procID}`).classList.add("active-btn")

    document.getElementById("proc1").addEventListener('click', function()
    {
        localStorage.setItem("address", "ws://172.16.98.100:50100")
        location.reload()
    })

    document.getElementById("proc2").addEventListener('click', function()
    {
        localStorage.setItem("address", "ws://172.16.98.102:50100")
        location.reload()
    })

    document.getElementById("proc3").addEventListener('click', function()
    {
        localStorage.setItem("address", "ws://172.16.98.101:50100")
        location.reload()
    })
}

function ClearFloorsFb()
{
    document.getElementById(`proc1`).classList.remove("active-btn")
    document.getElementById(`proc2`).classList.remove("active-btn")
    document.getElementById(`proc3`).classList.remove("active-btn")
}

function UpdateAvailableRoomsList(roomsList)
{
    var roomBtnsContainer = document.getElementById("roomBtnsContainer")
    for(let i = 0; i < roomsList.length; i++)
    {
        let roomBtn = document.createElement("div")
        
        if(document.getElementById("roomNameContainer").innerHTML == roomsList[i])
            roomBtn.classList.add("active-btn")

        roomBtn.classList.add('btn', 'btn-card-wide', 'btn-area-select', 'shadow-small', 'centered')
        roomBtn.id = i+1
        roomBtn.innerHTML = roomsList[i]

        roomBtnsContainer.appendChild(roomBtn)

        document.getElementById(roomBtn.id).addEventListener('click', function()
        {
            sendMessage("RoomChange:"+roomBtn.id)
        })
    }
}