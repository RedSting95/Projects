// nyitólap
const startButton = document.querySelector("#start");
const savedGameButton = document.querySelector("#savedGame");
const jatekoldal = document.querySelector("#jatekoldal");
const nyitolap = document.querySelector("#nyitolap");
const vegeDiv = document.querySelector("#vege");
const jatekDiv = document.querySelector("#jatek");
const player1=document.querySelector("#player1");
const player2=document.querySelector("#player2");
const divp1=document.querySelector("#divp1");
const divp2=document.querySelector("#divp2");
const p1name=document.querySelector("#p1name");
const p2name=document.querySelector("#p2name");
const figurep1=document.querySelector("#figurep1");
const figurep2=document.querySelector("#figurep2");
const p1svgs = document.querySelector("#p1svgs");
const p2svgs = document.querySelector("#p2svgs");
const babukDiv=document.querySelector("#babuk");
const pieces = Array.from(document.querySelectorAll("div.pcs"));
const piecessvg = Array.from(document.querySelectorAll("svg.pcssvg"));
const nyitButton = document.querySelector("#nyitButton");
const mentButton = document.querySelector("#mentButton");
const statp = document.querySelector("#statp");

let piecesData=[2,2,2,2,2,2,2,2];
let tableData=[];
let selectedItem="";
let selectedPcs="";
let shape="";
let moves=0;
let isVictory=false;
localStorage.setItem("p1v",0);
localStorage.setItem("p2v",0);
statp.innerHTML=`Győzelmek:<br>1.játékos: ${localStorage.getItem("p1v")}<br>2. játékos: ${localStorage.getItem("p2v")}`;

startButton.addEventListener("click", e => {
    emptyTable();
    nyitolap.style.display="none";
    jatekoldal.style.display="";
    vegeDiv.style.display="";
    tableGen(tableData);
    nameGen(player1.value,player2.value);
    for (let i = 0; i < pieces.length; i++) {
        pieces[i].innerHTML=2;
    }
    moves=0;
    figurep1.classList.add("actual");
    figurep2.classList.remove("actual");
})

savedGameButton.addEventListener("click", e => {
    nyitolap.style.display="none";
    jatekoldal.style.display="";
    vegeDiv.style.display="";

    moves=parseInt(localStorage.getItem("moves"));
    player1.value=localStorage.getItem("p1");
    player2.value=localStorage.getItem("p2");
    nameGen(player1.value,player2.value);
    let remainingpieces=JSON.parse(localStorage.getItem("pieces"));
    for (let i = 0; i < pieces.length; i++) {
        pieces[i].innerHTML=remainingpieces[i];
    }
    tableData=JSON.parse(localStorage.getItem("table"));

    tableGen(tableData);
    if (moves%2===0) {
        figurep1.classList.add("actual");
        figurep2.classList.remove("actual");
    } else {
        figurep2.classList.add("actual");
        figurep1.classList.remove("actual");
    }
})

jatekDiv.addEventListener("click",e => {
    if (!e.target.matches("td") || e.target.hasChildNodes()) return;
    if (isVictory) return;
    const x = e.target.cellIndex;
    const y = e.target.parentNode.rowIndex;
    
    if (isValidMove(e.target) && selectedPcs.innerHTML!=0) {
        tableData[y][x] = selectedItem;
        tableGen(tableData);
        selectedPcs.innerHTML=parseInt(selectedPcs.innerHTML)-1;

        if (isVictory) {
            if (moves%2===0) {
                alert(`${player1.value} megnyerte a játékot!"`);
                localStorage.setItem("p1v",parseInt(localStorage.getItem("p1v"))+1);
            } else {
                localStorage.setItem("p2v",parseInt(localStorage.getItem("p2v"))+1);
                alert(`${player2.value} megnyerte a játékot!"`);
            }
            statp.innerHTML=`Győzelmek:<br>1.játékos: ${localStorage.getItem("p1v")}<br>2. játékos: ${localStorage.getItem("p2v")}`;
            isVictory=false;
        } 
    
        if (selectedItem!=="") moves++;
        
        if (moves%2===0) {
            figurep1.classList.add("actual");
            figurep2.classList.remove("actual");
        } else {
            figurep2.classList.add("actual");
            figurep1.classList.remove("actual");
        }
    
        selectedItem="";
        selectedPcs="";
    }
})

babukDiv.addEventListener("click",e => {
    if (!e.target.matches('polygon') && !e.target.matches('circle')) return;

    const par = e.target.parentNode;

    selectedItem="";
    selectedPcs="";

    if (moves%2===1) {
        if (e.target.parentNode.getAttribute("fill")==="red") {
            selectedPcs=par.parentNode.lastChild.previousElementSibling;
            
                shape=e.target.parentNode.dataset.shape;
                selectedItem=`<svg width="50px" height="50px" fill="${e.target.parentNode.getAttribute("fill")}" data-shape="${e.target.parentNode.dataset.shape}">${e.target.parentNode.innerHTML}</svg>`;
                return;
        } 
    }
    if (moves%2===0){
        if (e.target.parentNode.getAttribute("fill")==="green") {
            selectedPcs=par.parentNode.lastChild.previousElementSibling;
            shape=e.target.parentNode.dataset.shape;
            selectedItem=`<svg width="50px" height="50px" fill="${e.target.parentNode.getAttribute("fill")}" data-shape="${e.target.parentNode.dataset.shape}">${e.target.parentNode.innerHTML}</svg>`;
            return;
        }
    }
})

nyitButton.addEventListener("click", e=> {
    nyitolap.style.display="";
    jatekoldal.style.display="none";
    vegeDiv.style.display="none";
})

mentButton.addEventListener("click", e=> {
    localStorage.clear();
    localStorage.setItem("p1",player1.value);
    localStorage.setItem("p2",player2.value);
    localStorage.setItem("moves", moves);
    localStorage.setItem("table", JSON.stringify(tableData));
    for (let i = 0; i < pieces.length; i++) {
        piecesData[i]=parseInt(pieces[i].innerHTML);
    }
    localStorage.setItem("pieces",JSON.stringify(piecesData));
    console.log(localStorage);
})

function tableGen(data) {
    let s="";
    s+=`<table>`;
    for (let i = 0; i < 4; i++) {
        s+=`<tr>`;
        for (let j = 0; j < 4; j++) {
            if (i<=1 && j<=1) s+=`<td bgcolor="lightyellow" data-sector="1">${data[i][j]}</td>`;
            if (i>=2 && j>=2) s+=`<td bgcolor="lightyellow" data-sector="4">${data[i][j]}</td>`;
            if (i<=1 && j>=2) s+=`<td bgcolor="lightgreen" data-sector="2">${data[i][j]}</td>`;
            if (i>=2 && j<=1)s+=`<td bgcolor="lightgreen" data-sector="3">${data[i][j]}</td>`;
        }
        s+=`</tr>`;
    }
    s+=`</table>`;
    jatekDiv.innerHTML=s;
}

function nameGen(p1,p2) {
    p1name.innerHTML=p1;
    p2name.innerHTML=p2;
}

function emptyTable() {
    for (let i = 0; i < 4; i++) {
        tableData[i]=[];
        for (let j = 0; j < 4; j++) {
            tableData[i][j] = [];  
        }
    }
    for (let i = 0; i < piecesData.length; i++) {
        pieces[i].innerHTML=piecesData[i];
    }
}

function isValidMove (td) {

    // checkrows
    const parent = td.parentNode;
    const rowTds = Array.from(parent.children);
    const rowShapes=[];
    const columnShapes=[];
    for (const td of rowTds) {
        if (td.hasChildNodes()) {
            rowShapes.push(td.firstChild.dataset.shape);
        }
    }
    
    // checkcolumns
    const cellIndex=td.cellIndex;
    const rows = Array.from(parent.parentNode.children);
    const columnTds=[];
    for (const row of rows) {
        const tds = Array.from(row.children);
        columnTds.push(tds[cellIndex]);
    }
    for (const td of columnTds) {
        if (td.hasChildNodes()) {
            columnShapes.push(td.firstChild.dataset.shape);
        }
    }
    
    // check sectors
    const sector = td.dataset.sector;
    const sectorTds=[];
    const sectorShapes=[];
    for (const row of rows) {
        const rowArray = Array.from(row.children);
        for (const td of rowArray) {
            if (td.dataset.sector===sector) sectorTds.push(td);
        }
    }
    for (const td of sectorTds) {
        if (td.hasChildNodes()) {
            sectorShapes.push(td.firstChild.dataset.shape);
        }
    }

    if (rowShapes.length>2 || columnShapes.length>2 || sectorShapes.length>2) isVictory=true;
    if (rowShapes.every(s => s!==shape) && columnShapes.every(s => s!==shape) && sectorShapes.every(s => s!==shape)) return true;
    return false;
}