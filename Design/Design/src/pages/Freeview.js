function InitializeFreeviewVariables()
{
    document.getElementById("returnBtn").addEventListener('click', function() {
        openSubpage("Home")
    })

    const BUTTONS_NUM = 100
    for(let i = 0; i < BUTTONS_NUM; i++)
    {
        if(document.getElementById(`srcBtn:${i}`) != null)
        {
            document.getElementById(`srcBtn:${i}`).addEventListener('click', function()
            {
                sendMessage(`srcBtn:${i}:Freeview`)
            })
        }
    }
}