let numOfCards = 0;

function InitializeHomeVariables()
{
    DrawAVCard()
    GetRoomUpdateCall()

    CardsIntroAnimation()
}

function DrawAVCard()
{
    $.get("home-page-cards/AV-Card/AV-Card.html", (data) => { $('#mainCardsContainer').html(data) });

    $.get("home-page-cards/Card-Label.html", (data) => 
    { 
        $('#cardLabels').html(data) 
        $('#containerLabelX').html('AV')
        $('#containerLabelX').attr('id', `cardLabel${numOfCards}`);

        numOfCards++;
        PopulateAVCard()
    });
}
function PopulateAVCard()
{
    let templateBtn = ''
    $.get("home-page-cards/AV-Card/AV-Source.html", (data) => { 
        templateBtn = data
        
        $.each(roomCoreData.menuItems, function (i, source) { 
            $('#avCard').append(templateBtn)
            $('#srcBtnX').text(`${source.menuItemName}`)
            if(roomCoreData.sourceSelected == source.menuItemName) $('#srcBtnX').addClass('active-btn')

            $('#srcBtnX').attr('id', `srcBtn${i}`)
        });

        ActivateSrcBtns()
    });
}
function ActivateSrcBtns()
{
    let pressDetection;
    if(panelType == "TSW") pressDetection = 'touchend'
    if(panelType == "iPad") pressDetection = 'click'
    else pressDetection = 'click'  

    $('*[id*=srcBtn]:visible').each(function() {
        $(this).on("click", function () {
            responseJSON = AjaxGETCall("ChangeSouceSelected", [roomCoreData.roomID, $(this).text()])
            UpdateSelectedSource(responseJSON.currentSource)

            srcID = $(this).attr('id').replace("srcBtn", "")
            if(roomCoreData.menuItems[srcID].menuItemPageAssigned != "")
                openSubpage(roomCoreData.menuItems[srcID].menuItemPageAssigned)
        });
    });
}
function UpdateSelectedSource(currentSource)
{
    //if source is the same return
    if(roomCoreData.sourceSelected == currentSource) return;

    roomCoreData.sourceSelected = currentSource
    $('*[id*=srcBtn]:visible').each(function() {
        if($(this).text() === roomCoreData.sourceSelected)
            $(this).addClass("active-btn");
        else
            $(this).removeClass("active-btn");
    });

    UpdateVolumeControls()
}

function ActivateTVBtns()
{
    $('#TVOn').on('click', () => {
        AjaxGETCall("TVOnBtnPress", [roomCoreData.roomID])
    })
    $('#TVOff').on('click', () => {
        AjaxGETCall("TVOffBtnPress", [roomCoreData.roomID])
    })

    $('#TVVolUp').on('touchend', () => {
        console.log("Up");
        AjaxGETCall("VolUpBtnPress", [roomCoreData.roomID])
    })

    $('#TVVolDown').on('touchend', () => {
        console.log("Down");
        AjaxGETCall("VolDownBtnPress", [roomCoreData.roomID])
    })
}

function CardsIntroAnimation()
{
    let mainCards = $('.main-card')

    $.each(mainCards, function (i, mainCard) { 
       $(mainCard).addClass('main-card-entry-animation')
    });

    setTimeout(() => {
        $.each(mainCards, function (i, mainCard) { 
           $(mainCard).removeClass('main-card-entry-animation')
        });
    }, 500);
}