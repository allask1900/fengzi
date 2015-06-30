var WindowInfo=function(){
        var scrollX=0,scrollY=0,width=0,height=0,contentWidth=0,contentHeight=0;
        if(typeof(window.pageXOffset)=='number'){
             scrollX=window.pageXOffset;
             scrollY=window.pageYOffset;
        }
        else if(document.body&&(document.body.scrollLeft||document.body.scrollTop)){
             scrollX=document.body.scrollLeft;
             scrollY=document.body.scrollTop;
        }
        else if(document.documentElement&&(document.documentElement.scrollLeft||document.documentElement.scrollTop)){
             scrollX=document.documentElement.scrollLeft;
             scrollY=document.documentElement.scrollTop;
        }

        if(typeof(window.innerWidth)=='number'){
             width=window.innerWidth;
             height=window.innerHeight;
        }
        else if(document.documentElement&&(document.documentElement.clientWidth||document.documentElement.clientHeight)){
             width=document.documentElement.clientWidth;
             height=document.documentElement.clientHeight;
        }
        else if(document.body&&(document.body.clientWidth||document.body.clientHeight)){
             width=document.body.clientWidth;
             height=document.body.clientHeight;
        }

        if(document.documentElement&&(document.documentElement.scrollHeight||document.documentElement.offsetHeight)){
             if(document.documentElement.scrollHeight>document.documentElement.offsetHeight){
                  contentWidth=document.documentElement.scrollWidth;
                  contentHeight=document.documentElement.scrollHeight;
             }
             else{
                  contentWidth=document.documentElement.offsetWidth;
                  contentHeight=document.documentElement.offsetHeight;
             }
        }
        else if(document.body&&(document.body.scrollHeight||document.body.offsetHeight)){
             if(document.body.scrollHeight>document.body.offsetHeight){
                  contentWidth=document.body.scrollWidth;
                  contentHeight=document.body.scrollHeight;
             }
             else{
                  contentWidth=document.body.offsetWidth;
                  contentHeight=document.body.offsetHeight;
             }
        }
        else{
             contentWidth=width;
             contentHeight=height;
        }
         if(height>contentHeight){
            height=contentHeight;
         }
         if(width>contentWidth){
            width=contentWidth;
         }
         return {"Width":width,"Height":height,"ContentWidth":contentWidth,"ContentHeight":contentHeight,"ScrollX":scrollX,"ScrollY":scrollY};
  };
  
 function Trim(str){  
    // 用正则表达式将前后空格  
    // 用空字符串替代。  
    return str.replace(/(^\s*)|(\s*$)/g, "");  
}
  
  function ShowBottomWindow(url,msgHeight,Id,btn,title){ 
    var msgObj=document.getElementById(Id);
    if(msgObj){
        document.body.removeChild(msgObj);
    }
    var bordercolor;
    bordercolor='#738ECE';
    titlecolor='#99CCFF';
    var sWidth,sHeight;
    var windowInfo = new WindowInfo();
    sWidth=windowInfo.Width;//document.body.offsetWidth;

    sHeight=windowInfo.Height;//screen.height;
    msgObj=document.createElement('div');
    msgObj.setAttribute('id',Id);
 
    msgObj.style.border='2px solid '+ bordercolor;
    msgObj.style.position = 'absolute';
    msgObj.style.background='#ffffff';
    msgObj.style.top =sHeight - msgHeight +'px';
    msgObj.style.left ='0px';
    msgObj.style.marginLeft  ='0px';
    msgObj.style.width = (sWidth - 8) + 'px';
    msgObj.style.height =msgHeight + 'px';

    msgObj.style.lineHeight ='15px';
    msgObj.style.zIndex ='10001';
    msgObj.style.fontSize ='14px';
    
    var titleDiv  = document.createElement("div");
    titleDiv.setAttribute('id',Id+'TitleDiv');
    titleDiv.style.border='0px solid '+ bordercolor;
    titleDiv.style.background='#C7E1F4';
    titleDiv.style.width = (sWidth - 8) + 'px';
    titleDiv.style.height = '28px';
    titleDiv.style.Left="0px";
    
    var img = document.createElement("img");
    img.src="./Images/0No.ico";
    img.style.position = 'absolute';
    img.id = Id+"btnClose";
    img.style.top = "4px";
    img.style.left = (sWidth-26)+"px";
    img.title="关闭";
    img.onclick=removeObj;
    titleDiv.appendChild(img);
    
    if(title != null && title != 'undefined'){
        var titles = document.createElement("span");
        titles.innerText = title;
        titles.style.color='#555555';
        titles.style.fontWeight='bold';
        titles.style.position = 'absolute';
        titles.style.top = "4px";
        titles.style.left = "8px";
        titles.style.lineHeight= "26px";
        titleDiv.appendChild(titles);
    }
    
    function removeObj(){
    document.body.removeChild(msgObj);
     if(document.getElementById(btn))
     {
        document.getElementById(btn).click();
        }
        
    }
    

    var txt=document.createElement('p');
    txt.setAttribute('id',Id+'msgTxt');
    txt.style.paddingLeft="0px";
    txt.innerHTML="<iframe frameborder=0 src="+url+" width="+(sWidth-9)+" height="+(msgHeight-28)+" scrolling=no></iframe>";
    msgObj.appendChild(titleDiv);
    msgObj.appendChild(txt);
    document.body.appendChild(msgObj);
}

 function ShowMsg(str){ 
    var msgw,msgh,bordercolor;
    msgw=400;
    msgh=120;
    bordercolor='#738ECE';
    titlecolor='#99CCFF';
    var sWidth,sHeight;
    var windowInfo = new WindowInfo();
    sWidth=windowInfo.Width; //document.body.offsetWidth;
    sHeight=windowInfo.Height; //document.body.clientHeight;

    var bgObj=document.createElement('div');
    bgObj.setAttribute('id','bgDiv');
    bgObj.style.position='absolute';
    bgObj.style.top='0';
    bgObj.style.background='#f4f4f4';
    bgObj.style.filter='progid:DXImageTransform.Microsoft.Alpha(style=2,opacity=25,finishOpacity=75';
    bgObj.style.opacity='0.6';
    bgObj.style.left='0';
    bgObj.style.width='100%';
    bgObj.style.height=sHeight + 'px';
    bgObj.style.zIndex = '10000';
    document.body.appendChild(bgObj);
    
    var msgObj=document.createElement('div');
    msgObj.setAttribute('id','msgDiv');
 
//    var boxw=parseInt(msgh)+30;
    var msgright=(sWidth-msgw)/2;
    var msgtop=(sHeight-msgh)/2;
    
    
    msgObj.style.border='2px solid '+ bordercolor;
    msgObj.style.position = 'absolute';
    msgObj.style.left = msgright + 'px';
    msgObj.style.background='#ffffff';
    msgObj.style.top = msgtop + 'px';
//    msgObj.style.marginLeft =msgright+"px";
//    msgObj.style.marginTop = -msgtop+'px';
    msgObj.style.width = msgw + 'px';
    msgObj.style.height =msgh + 'px';
    msgObj.style.textAlign = 'center';
    msgObj.style.lineHeight ='25px';
    msgObj.style.zIndex ='10001';
    msgObj.style.fontSize ='14px';
    
    var button=document.createElement("input");
    button.setAttribute("type","button");
    button.setAttribute("value","确认");
    button.style.width="60px";
    button.style.align="center";
    button.style.background=bordercolor;
    button.style.border="1px solid "+ bordercolor;
    button.style.color="white";
    button.onclick=removeObj;
    
    function removeObj(){
    document.body.removeChild(bgObj);
    document.body.removeChild(msgObj);
     document.body.focus(); 
    }
    document.body.appendChild(msgObj);

    var txt=document.createElement('p');
    txt.style.margin='30px;'
    txt.setAttribute('id','msgTxt');
    txt.innerHTML=str;
    
    document.getElementById('msgDiv').appendChild(txt);
    var txt=document.createElement("p");
    txt.innerHTML="";
    txt.id="close";
    document.getElementById("msgDiv").appendChild(txt);
    document.getElementById("close").appendChild(button);
     document.body.focus(); 
    }
//url,指向地址页面
//msgw，弹出页面宽度，填写数字
//msgh，弹出页面高，填写数字
//btn,回调控件按钮Click事件的id，当点击关闭弹出框时触发按钮click事件
//title，页面标题
//isScroll，是否在弹出页面显示滚动条，值为：yes或no
function ShowDiv(url,msgw,msgh,btn,title,isScroll){ 
    var bordercolor;
    msgh += 25;
    bordercolor='#738ECE';
    titlecolor='#99CCFF';
    var sWidth,sHeight;
    var windowInfo = new WindowInfo();
    sWidth=windowInfo.Width;//document.body.offsetWidth;

    sHeight=windowInfo.Height;//screen.height;
    var bgObj=document.createElement('div');
    bgObj.setAttribute('id','bgDiv');
    bgObj.style.position='absolute';
    bgObj.style.top='0';
    bgObj.style.background='#f4f4f4';
    bgObj.style.filter='progid:DXImageTransform.Microsoft.Alpha(style=2,opacity=25,finishOpacity=75';
    bgObj.style.opacity='0.6';
    bgObj.style.left='0';
    bgObj.style.width='100%';
    bgObj.style.height=sHeight + 'px';

    bgObj.style.zIndex = '10000';
    document.body.appendChild(bgObj);
    
    var boxw=parseInt(msgh)+30;
    var msgright=(sWidth-parseInt(msgw))/2;
    var msgtop=(sHeight-parseInt(msgh))/2;
    
    if(sHeight<520)
        msgtop=0;
        
    var msgObj=document.createElement('div');
    msgObj.setAttribute('id','msgDiv');
    
    // move div 
    msgObj.onmousedown= function(){moveStart(this,event);};
    msgObj.onmousemove=function(){moving(this,event);};
    msgObj.onmouseup=function(){moveEnd(this);};


    msgObj.style.border='2px solid '+ bordercolor;
    msgObj.style.position = 'absolute';
    msgObj.style.background='#ffffff';
    msgObj.style.top =msgtop+'px';
    msgObj.style.marginLeft  =msgright+'px';
    msgObj.style.zIndex = '100001';
//    msgObj.style.marginTop = -155+document.documentElement.scrollTop+'px';
    msgObj.style.width = msgw + 'px';
    msgObj.style.height =boxw + 'px';

    msgObj.style.lineHeight ='15px';
//    msgObj.style.zIndex ='10001';
    msgObj.style.fontSize ='14px';
    
    
    var titleDiv  = document.createElement("div");
    titleDiv.setAttribute('id','TitleDiv');
    titleDiv.style.border='0px solid '+ bordercolor;
    titleDiv.style.background='#C7E1F4';
    titleDiv.style.width = (msgw) + 'px';
    titleDiv.style.height = '28px';
    titleDiv.style.Left="0px";
//    titleDiv.style.textAlign = "right";
//    titleDiv.appendChild(bgIFame);
//    titleDiv.innerHTML = "<iframe frameborder=\"0\" style=\"position: absolute; background-color:#f4f4f4; visibility: inherit; top: 0px;left: 0px; width: 290px; height: 120px; z-index: -1; filter='progid:dximagetransform.microsoft.alpha(style=0,opacity=0)';\"></iframe>";
    var img = document.createElement("img");
    img.src="../Images/0No.ico";
    img.style.position = 'absolute';
    img.id = "btnClose";
    img.style.top = "4px";
    img.style.left = (msgw-26)+"px";
    img.title="关闭";
    img.onclick=removeObj;
    titleDiv.appendChild(img);
    
    if(title != null && title != 'undefined'){
        var titles = document.createElement("span");
        titles.innerText = title;
        titles.style.color='#555555';
        titles.style.fontWeight='bold';
        titles.style.position = 'absolute';
        titles.style.top = "4px";
        titles.style.left = "8px";
        titles.style.lineHeight= "26px";
        titleDiv.appendChild(titles);
    }
    
    function removeObj(){
        msgObj.removeChild(txt);
        document.body.removeChild(bgObj);
        document.body.removeChild(msgObj);
        if(document.getElementById(btn)){
            document.getElementById(btn).click();
        }
        document.body.focus();    
    }
    

    var txt=document.createElement('p');
    txt.setAttribute('id','msgTxt');
    txt.style.paddingLeft="0px";
    if(isScroll != 'yes' && isScroll != 'no'){
        isScroll = 'no';
    }
    txt.innerHTML="<iframe id='iframeShow' frameborder=0 src="+url+" width="+msgw+" style='z-index:100001;' height="+msgh+" scrolling="+isScroll+"></iframe>";
    msgObj.appendChild(titleDiv);
    msgObj.appendChild(txt);
    document.body.appendChild(msgObj);
    document.getElementById("iframeShow").document.documentElement.document.body.focus()
}

/************************/
//function ShowIframeDiv(){
//return;
//    if(isShowIframeDiv) return;
//    document.write(" <div id=\"divUserListOut\" style=\"border: #9CB2C6 2px solid; overflow-y: auto; width: 120px;"+
//                   "    background-color: #FFFFFF; position: absolute; display: none;\"> "+
//                   "     <iframe id=\"ShowIframeDivIframe1\" frameborder=\"0\" scrolling=\"no\" style=\"background-color: #FFFFFF; width: 82px; height: 120px;\">"+
//                   "     </iframe>"+
//                   "     <div id=\"divUserList\" style=\"background-color: #FFFFFF; position: absolute; top:0px; left:0px; visibility: visible;"+
//                   "         z-index: 1000;\">"+
//                   "     </div>"+
//                   " </div>");
//   isShowIframeDiv = true;
//}
var vol = null;
//var isShowIframeDiv = false;
function GetUserList(obj,url){
    var div2=document.getElementById("divUserList"),ifrm = document.getElementById("ShowIframeDivIframe1");
    var ol =document.getElementById("divUserListOut");;
     if(ol ==null || ol == "undefined"){
        ol = document.createElement("div");
        ol.id="divUserListOut"
        ol.style.border = "#9CB2C6 2px solid";
//        ol.style.overflowY = "auto";
        ol.style.width = "120px";
        ol.style.backgroundcolor = "#FFFFFF";
        ol.style.position = "absolute";
        ol.style.display="none";
        ifrm = document.createElement("iframe");
        ifrm.frameborder="0";
        ifrm.style.width="112px";
        ifrm.style.height="120px";
        ifrm.id="ShowIframeDivIframe1";
        ifrm.scrolling = "no";
        div2 = document.createElement("div");
        div2.id="divUserList"
        div2.style.backgroundcolor = "#FFFFFF";
          div2.style.background = "#FFFFFF";
        div2.style.position = "absolute";
        div2.style.top="0px";
        div2.style.left = "0px";
        div2.style.visibility = "visible";
        div2.style.overflowY = "auto";
        div2.style.zIndex = "1000"; 
        div2.style.height = "120px";
        ol.appendChild(ifrm);
        ol.appendChild(div2);
        document.body.appendChild(ol);
    }
    var ttop1  = obj.offsetTop;    
  	var thei1  = obj.clientHeight; 
  	var tleft1 = obj.offsetLeft; 
  	var ttyp1  = obj.type; 
  	var tobj = obj;
    ol.style.width = obj.clientWidth + "px";
    while (tobj = tobj.offsetParent){
  	    ttop1+=tobj.offsetTop; 
  	    tleft1+=tobj.offsetLeft;
  	} 
    ol.style.top =  ttop1+thei1 + 6  ;
    ol.style.left = tleft1;     
    
    ifrm.style.width=ol.style.width;//(ol.clientWidth == 0 ? obj.clientWidth  : ol.clientWidth) + "px";
    div2.style.width =ol.style.width;// ifrm.style.width;

    var tw = ol.clientWidth;//  ifrm.style.width;//parseInt(ol.style.width) - 17;
//    var  re = /\w+/g;
//    var r =re.exec(obj.value.toLowerCase()); // 在字符串 s 中查找匹配。      
     IsShow(true,ol,ifrm);
//    if(r == null) r = "";
     var o = div2;
    if(vol == null || vol != obj.value || (vol == obj.value && vol == "")){
       
        var vol=v = obj.value.toLowerCase();        
        ClearAll(o,false);
        if(obj != null){
             var ddlObj = document.getElementById(url);
             if(ddlObj != null && ddlObj != 'undefined'){
                var options = ddlObj.options;
                var len = options.length;
                
                if(len>1){
                    tw = len < 8? tw :(parseInt(ol.style.width) - 16+"px");
                    for(var i=0;i<len;i++){
                        if(options[i].value.toLowerCase().indexOf(v)==0 || options[i].text.toLowerCase().indexOf(v)==(options[i].text.indexOf('(')+1))
                         CreateDiv(options[i].value,options[i].text);
                    }
                }
             }
             else{
                $.post(url,{ 'username':v }, DoResult);    
             }
        }
    }
    
    function DoResult(result){  
        if ( result == "")
            return;
        
        var arr = result.split("|"); 
        var len = arr.length;
        var str = "";
        tw = len < 8? tw :(parseInt(ol.style.width) - 16+"px");
        for(var i=0;i<len;i++){
            var vl = arr[i];
            if(vl!=""){
                var ul = vl.split(",");
                CreateDiv(ul[0],ul[1]);
            }
        }
    }
    
    function CreateDiv(v,t){
        var tv = document.createElement("div");
        tv.id=v;
        tv.title = t;
        tv.innerHTML = "&nbsp;"+t;
        tv.style.width = tw;
        tv.style.height = "20px";
        tv.style.textalign="left";
        tv.onmouseover =function(){ setBackGroupColor(this,'#0A246A')};
        tv.onmouseout =function(){ setBackGroupColor(this,'#FFFFFF')};
        tv.onclick = function(){SetTextValue(obj,this,ol,ifrm);}
        o.appendChild(tv);
    }
}


 
 function setBackGroupColor(obj,col){
    obj.style.background = col;
    if(col == "#FFFFFF"){
        obj.style.color="#000000";
    }
    else{
        obj.style.color="#FFFFFF";
    }
 }
 
 function SetTextValue(obj,o,ol,ifrm){
    obj.value = o.title; 
    IsShow(false,ol,ifrm);
    document.body.removeChild(ol);
 }
 
 function ClearAll(obj,bl){
    var divs = obj.getElementsByTagName("div"); 
    var len = divs.length;
    for(var i=0;i<len;i++){
        divs[0].removeNode(true);
    } 
  }
 function IsShow(bl,ol,ifrm){
    var obj = ol;
    if(bl){
        obj.style.display="";
        ifrm.style.display="";
    }
    else{
        obj.style.display="none";
        ifrm.style.display="none";
    }
 }
 
 function FixFrameSize(w,h,ifrm){
    var f = ifrm;
    f.style.width = w ;
    f.style.height = h+ "px";
 }
/*******************************************/

// Mouse right button menu  rem begin
var menuname = 'rightmenu';

function getEvent(){     
       if(document.all)    return window.event;        
       func=getEvent.caller;            
       while(func!=null){    
           var arg0=func.arguments[0];
           if(arg0){
               if((arg0.constructor==Event || arg0.constructor ==MouseEvent)
                   || (typeof(arg0)=="object" && arg0.preventDefault && arg0.stopPropagation)){    
                   return arg0;
               }
           }
           func=func.caller;
       }
      return null;
}


function showmenuie5() {
    var ie5menu = document.getElementById(menuname);
    
    var event=arguments[0] || window.event;    
    
    var rightedge = document.body.clientWidth-event.clientX;
    var bottomedge = document.body.clientHeight-event.clientY;
	    
    if (rightedge <ie5menu.offsetWidth)
	    ie5menu.style.left = (document.body.scrollLeft + event.clientX - ie5menu.offsetWidth) + 'px';  
    else
	    ie5menu.style.left = (document.body.scrollLeft + event.clientX) + 'px';
    
    if (bottomedge <ie5menu.offsetHeight)
	    ie5menu.style.top = (document.body.scrollTop + event.clientY - ie5menu.offsetHeight) + 'px';
    else
	    ie5menu.style.top = (document.body.scrollTop + event.clientY) + 'px';

    
    ie5menu.style.visibility = "visible";
    return false;
}

function hidemenuie5() {
    var ie5menu = document.getElementById(menuname);
    ie5menu.style.visibility = "hidden";
}

function highlightie5(evt) {

    var event=evt || window.event;
    var element=event.srcElement || event.target;
    
    if (element.className == "menuitems") {
        element.style.backgroundColor = "highlight";
        element.style.color = "white";
   }
}

function lowlightie5(evt) {
    var event=evt || window.event;
    var element=event.srcElement || event.target;
    
    if (element.className == "menuitems") {
        element.style.backgroundColor = "";
        element.style.color = "black";
    }
}
// Mouse right button menu  rem end




function GetValue(id)
{
    return document.getElementById(id).value;
}


function CheckValue(o){
    if(o!=null && o != 'undefined'){
        var len = o.length;
        var tmpElement = null;
        var titleStr;
        var elemType = "";
        var elemValue = "";
        
        for(var i=0;i<len;i++){
            tmpElement = o[i];
            if(tmpElement != null && tmpElement.title.length>0 && tmpElement.title.indexOf('^')>-1){                    
                titleStr = tmpElement.title.split('^');
                elemValue = tmpElement.value;

                if(tmpElement.tagName == "INPUT" && titleStr.length>1){
                    if(titleStr[1].length>0 && elemValue.length==0){
                        try{
                            o[i].focus();
                        }
                        catch(ex){}
                        alert(titleStr[0])
                        return false;
                    }
                    else if( elemValue.length>0){
                        if(titleStr.length==3){
                           elemType = titleStr[2].split('!');
                           if(elemType.length==2){
                               if( elemType[1]=="datetime"){
                                    var str=/^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|2468][048]|[3579][26])00))-0?2-29-))( (([[0-1]?\d)|(2[0-3])):[0-5]?\d:[0-5]?\d)?$/;
                                    if(!str.test(elemValue) ){
                                         o[i].value="";
                                        try{
                                            o[i].focus();
                                            alert("请输入正确的日期类型！");
                                        }
                                        catch(ex){
                                            alert(titleStr[0])
                                        }
                                        return false;
                                    }
                               }
                               else if( elemType[1]=="int" ){
                                    if(isNaN(elemValue)){
                                         o[i].value="";
                                        try{
                                            o[i].focus();
                                            alert("请输入数字类型！");
                                        }
                                        catch(ex){
                                            alert(titleStr[0])
                                        }
                                       
                                        
                                        return false;
                                    }
                               }
                           }
                         }
                    } 
                }
                else if(tmpElement.tagName == "SELECT" && titleStr.length>1){
                     if(titleStr[1].length>0 && elemValue.length==0){
                        try{
                            if(o[i].disabled == false && o[i].style.display != "none"){
                                o[i].focus();
                            }
                        }
                        catch(Ex){
                        }
                        alert(titleStr[0])
                        return false;
                    }
               }
        }
    }
} 
return true;
}




//Interface
function MyAPI()
{
	this.Type = {// mark web browser ---------- learn from prototype framework 
		IE:     !!(window.attachEvent && !window.opera),
		Opera:  !!window.opera,
		WebKit: navigator.userAgent.indexOf('AppleWebKit/') > -1,
		Gecko:  navigator.userAgent.indexOf('Gecko') > -1 && navigator.userAgent.indexOf('KHTML') == -1,
		MobileSafari: !!navigator.userAgent.match(/Apple.*Mobile.*Safari/)
	}
	this.api = this.getInstance();

	return this;
}

//IE implementation
function IEAPI()
{
	return this;
}

//W3C implementataion
function W3CAPI()
{
	return this;
}



//factory method
MyAPI.prototype.getInstance=function()
{
	if(this.Type.IE)
		return new IEAPI();
	else
		return new W3CAPI();
	
};



//method moveTo detail implementation
MyAPI.prototype.moveTo=function(obj,iX,iY)
{
	this.api.moveTo(obj,iX,iY);
}
IEAPI.prototype.moveTo=function(obj,iX,iY)
{
	if(iX!=null) obj.style.pixelLeft=iX;
	if(iY!=null) obj.style.pixelTop=iY;
}
W3CAPI.prototype.moveTo=function(obj,iX,iY)
{
	if(iX!=null) obj.style.left=iX+"px";
	if(iY!=null) obj.style.top=iY+"px";
}



//method getLeft detail implementation
MyAPI.prototype.getLeft=function(obj)
{
	return this.api.getLeft(obj);
}
IEAPI.prototype.getLeft=function(obj)
{
	return obj.style.pixelLeft;
}
W3CAPI.prototype.getLeft=function(obj)
{	
	return parseInt(obj.style.left);
}



//method getTop detail implementation
MyAPI.prototype.getTop=function(obj)
{
	return this.api.getTop(obj);
}
IEAPI.prototype.getTop=function(obj)
{
	return obj.style.pixelTop;
}
W3CAPI.prototype.getTop=function(obj)
{
	return parseInt(obj.style.top);
}

var myAPI = new MyAPI();
var moveable = false;
var xOff = 0;
var yOff = 0;

function moveStart(obj,e){
	moveable = true;
	xOff = myAPI.getLeft(obj) - e.clientX;
	yOff = myAPI.getTop(obj) - e.clientY;
	obj.setCapture();
}

function moving(obj,e){
	if(moveable){
		myAPI.moveTo(obj, xOff + e.clientX, yOff + e.clientY);
	}
}

function moveEnd(obj){
	moveable = false;
	obj.releaseCapture();
}