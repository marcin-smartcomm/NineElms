function InitializeSkyVariables()
{
    document.getElementById("returnBtn").addEventListener('click', function() {
        openSubpage("Home")
    })

    //Btns on page = 0 - 30
    const BUTTONS_NUM = 31
    for(let i = 0; i < BUTTONS_NUM; i++)
    {
        document.getElementById(`srcBtn:${i}`).addEventListener('click', function()
        {
            sendMessage(`srcBtn:${i}:Sky`)
        })
    }
}