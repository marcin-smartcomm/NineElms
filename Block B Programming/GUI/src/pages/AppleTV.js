function InitializeAppleTVVariables()
{
    $('#returnBtn').on('click', () => {
        openSubpage("Home")
    })

    const BUTTONS_NUM = 100
    for(let i = 0; i < BUTTONS_NUM; i++)
    {
        if(document.getElementById(`srcBtn:${i}`) != null)
        {
            document.getElementById(`srcBtn:${i}`).addEventListener('touchend', () =>
            {
                AjaxGETCall("ATVCtrl", [document.getElementById(`srcBtn:${i}`).dataset.btn_name])
            })
        }
    }
}