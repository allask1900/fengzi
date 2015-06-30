 

var oProgressLayer=null;
/************************************************************************************************
// 设置网页上所有元素为不可响应事件，以及设置鼠标光标为wait
*************************************************************************************************/
function SetBusy(){
 

for( i = 0; i < document.forms[0].elements.length; i++ )
{
	curObj = document.forms[0].elements(i);
	try
	{
		if( curObj.type=="button" || curObj.type == "submit" )
		{
			curObj.style.cursor='wait'; 
			curObj.onmousedown=  function(){return false;}	
			curObj.onclick=  function(){return false;}	
		}
	}
	catch(e){;}		
}

}
/************************************************************************************************
// 恢复网页上所有元素可以响应事件，以及设置鼠标光标默认光标
*************************************************************************************************/
function ReleaseBusy(){
for(var iCnt=0;iCnt<document.all.length;iCnt++){
try{document.all[iCnt].style.cursor=document.all[iCnt].oldCursor;}catch(e){;}
try{document.all[iCnt].onmousedown=document.all[iCnt].oldonmousedown;}catch(e){;}
try{document.all[iCnt].onclick=document.all[iCnt].oldonclick;}catch(e){;}
try{document.all[iCnt].onmouseover=document.all[iCnt].oldonmouseover;}catch(e){;}
try{document.all[iCnt].onmousemove=document.all[iCnt].oldonmousemove;}catch(e){;}
try{document.all[iCnt].onkeydown=document.all[iCnt].oldonkeydown;}catch(e){;}
try{document.all[iCnt].oncontextmenu=document.all[iCnt].oldoncontextmenu;}catch(e){;}
try{document.all[iCnt].onselectstart=document.all[iCnt].oldonselectstart;}catch(e){;}
}
}
/************************************************************************************************
// 关闭“正在处理"对话框
*************************************************************************************************/
function HideProgressInfo()
{
	if(oProgressLayer)
	{
	//ReleaseBusy();

		oProgressLayer.removeNode(true);
		oProgressLayer=null;
	}
	if(window.frames.beyonbit_progress.document.oProgressLayer)
	{
		
		window.frames.beyonbit_progress.document.oProgressLayer.removeNode(true);
		window.frames.beyonbit_progress.document.oProgressLayer=null;
		document.all.beyonbit_progress.style.display = 'none';
	}
}
 
/************************************************************************************************
// 显示“正在处理”对话框 (样式二) 进度条样式
*************************************************************************************************/
//建立iframe
document.writeln('<iframe id="beyonbit_progress" scrolling="no" frameborder="0" style="position: absolute; width: 144; height: 211; z-index: 9998; display: none"></iframe>');

 

function ShowProgressInfo1(){

if(oProgressLayer) return;
window.frames.beyonbit_progress.document.body.style.margin = '0px';
with(document.all.beyonbit_progress.style)
{
	width='230px';
	height='80px';
	left=(document.body.clientWidth-230)>>1;
	top=(document.body.clientHeight-30)>>1;
}

oProgressLayer = window.frames.beyonbit_progress.document.createElement('DIV');

with(oProgressLayer.style){
width='230px';
height='80px';
position='absolute';
//left=(document.body.clientWidth-230)>>1;
//top=(document.body.clientHeight-70)>>1;
//backgroundColor='#e7e7e7';
//borderLeft='solid 1px silver';
//borderTop='solid 1px silver';
//borderRight='solid 1px gray';
//borderBottom='solid 1px gray';
fontWeight='700';
fontSize='13px';
zIndex='999';
}
strImage = '<span style="cursor:default;width:12px;height:12px;"><img src="about:blank" width="1" height="1" border="0" /></span>' ;
strInnerHTML = '<table border="0" cellspacing="0" style="border-style:solid;border-width:1px;border-color:#73A2D6;" cellpadding="0" width="100%" height="100%">'+
'<tr><td style="cursor:default;font-size:9pt;height:26px;background-color:#73A2D6;" align="center" valign="middle">'+
'正在处理数据，请稍候……</td></tr>'+
'<tr><td style="text-align:center;vertical-align:middle;height:48px;">'+
'<span style="border:solid 1px #808080;padding:1px;height:14px;margin-top:5px;">'+
'<span style="width:150px;height:12px;border:none;padding:-1px;overflow:hidden;">'+
'<marquee style="width:150px;" direction="right" scrollamount="10">'+
'<span style="padding:1px;cursor:default;text-align:right;width:130px;height:10px;background-color:#0D4F77;filter:Alpha(startX=0,startY=0, finishX=144, finishY=0,style=1,opacity=0,finishOpacity=100);">';
for( i = 1 ; i <= 12 ; i++)
{
	strInnerHTML = strInnerHTML + strImage;
}
strInnerHTML = strInnerHTML +
'</span>'+
'</marquee>'+
'</span>'+
'</span></td></tr></table>';
oProgressLayer.innerHTML = strInnerHTML;
//oProgressLayer.innerHTML='<img src="/Web/images/loading.gif"/>';

window.frames.beyonbit_progress.document.body.appendChild(oProgressLayer);
document.all.beyonbit_progress.style.display = 'block';
window.frames.beyonbit_progress.document.close();  //解决ie进度条不结束的问题 

 SetBusy();

}