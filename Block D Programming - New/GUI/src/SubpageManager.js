let backBtn;
let previousSubpage;
let currentSubpage;
let blankOutBtnsVis = false;
var popupOpen = false;

function openSubpage(file)
{
  document.getElementById("subpageSection").classList.add("transitionIn")
  
  if(currentSubpage != null)
    previousSubpage = currentSubpage;
  else
    previousSubpage = "ScreenSaver";

  currentSubpage = file;

  var rawFile = new XMLHttpRequest();
  rawFile.open("GET", './pages/'+file+'.html', false);
  rawFile.onreadystatechange = function ()
  {
      if(rawFile.readyState === 4)
      {
          if(rawFile.status === 200 || rawFile.status == 0)
          {
              var allText = rawFile.responseText;
              document.querySelector('#subpageSection').innerHTML = allText;
          }
      }
  }
  rawFile.send(null);
  rawFile.DONE;
  
  InitializeSubpageVariables(file);

  setTimeout(ClearTransition, 500);
}

function OpenPopUp(file)
{
  //if popup already open, return
  if($(`#${file}`).length > 0) return;

  var rawFile = new XMLHttpRequest();
  rawFile.open("GET", './popups/'+file+'.html', false);
  rawFile.onreadystatechange = function ()
  {
      if(rawFile.readyState === 4)
      {
          if(rawFile.status === 200 || rawFile.status == 0)
          {
            var allText = rawFile.responseText;
            var popup = document.createElement("div")
            popup.setAttribute("id", `${file}`)
            popup.innerHTML = allText;
            document.querySelector('#subpageSection').appendChild(popup);
          }
      }
  }
  rawFile.send(null);
  rawFile.DONE;

  InitializeSubpageVariables(file);
}

function ClosePopUp(file)
{
  if ($(`#${file}`).length > 0)
    $(`#${file}`).remove();
}

function ClearTransition()
{
  document.getElementById("subpageSection").classList.remove("transitionIn");
}

function InitializeSubpageVariables(pageToInitialize)
{
  if(pageToInitialize == "ScreenSaver") InitializeScreenSaverVariables()
  if(pageToInitialize == "PowerOff") InitializePowerOffVariables()
  if(pageToInitialize == "Home") InitializeHomeVariables()
  if(pageToInitialize == "Sky") InitializeSkyVariables()
  if(pageToInitialize == "Freeview") InitializeFreeviewVariables()
  if(pageToInitialize == "AreaSelect") InitializeAreaSelectVariables()
}