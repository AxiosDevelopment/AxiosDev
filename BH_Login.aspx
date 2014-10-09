<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BH_Login.aspx.vb" Inherits="BH20.Login" %>
<!DOCTYPE html>

<html>
<head>
<meta http-equiv="Content-Language" content="en-us"/>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0"/>
<meta name="ProgId" content="FrontPage.Editor.Document"/>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252"/>
<title>BeeHive Login</title>
</head>
<script type="text/javascript" language="JavaScript">
<!--
    function MM_preloadImages() { //v3.0
        var d = document; if (d.images) {
            if (!d.MM_p) d.MM_p = new Array();
            var i, j = d.MM_p.length, a = MM_preloadImages.arguments; for (i = 0; i < a.length; i++)
                if (a[i].indexOf("#") != 0) { d.MM_p[j] = new Image; d.MM_p[j++].src = a[i]; } 
        }
    }

    function MM_swapImgRestore() { //v3.0
        var i, x, a = document.MM_sr; for (i = 0; a && i < a.length && (x = a[i]) && x.oSrc; i++) x.src = x.oSrc;
    }

    function MM_findObj(n, d) { //v4.01
        var p, i, x; if (!d) d = document; if ((p = n.indexOf("?")) > 0 && parent.frames.length) {
            d = parent.frames[n.substring(p + 1)].document; n = n.substring(0, p);
        }
        if (!(x = d[n]) && d.all) x = d.all[n]; for (i = 0; !x && i < d.forms.length; i++) x = d.forms[i][n];
        for (i = 0; !x && d.layers && i < d.layers.length; i++) x = MM_findObj(n, d.layers[i].document);
        if (!x && d.getElementById) x = d.getElementById(n); return x;
    }

    function MM_swapImage() { //v3.0
        var i, j = 0, x, a = MM_swapImage.arguments; document.MM_sr = new Array; for (i = 0; i < (a.length - 2); i += 3)
            if ((x = MM_findObj(a[i])) != null) { document.MM_sr[j++] = x; if (!x.oSrc) x.oSrc = x.src; x.src = a[i + 2]; }
    }


    function popUp(url) {
        sealWin = window.open(url, "win", 'toolbar=0,location=0,directories=0,status=1,menubar=1,scrollbars=1,resizable=1,width=500,height=450');
        self.name = "mainWin";
    }

    function SubmitLoginID() {
        if (document.fSubmitID.UN.value == '')
        { alert("Please enter User Name"); }
        else if (document.fSubmitID.PW.value == '')
        { alert("Please enter Password"); }
        else
        { document.fSubmitID.submit(); }
    }
	

//-->
</script>
<body onload="MM_preloadImages('img/Submit2ON.gif')">

<table border="0" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" bordercolor="#111111" width="100%" id="AutoNumber1" height="400">
  <tr>
    <td width="100%" align="middle" valign="top">
    <table border="0" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" bordercolor="#111111" width="700" id="AutoNumber2">
      <tr>
        <td align="left" width="700" height="400" valign="top">
        <div align="left">
          <table border="0" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" bordercolor="#111111" width="666" id="AutoNumber3">
            <tr>
              <td width="50" height="50">&nbsp;</td>
              <td width="566" height="50" align="left" valign="top">
			
              	<img src="img/BHLogoTrans.gif" border="0">
              </td>
            </tr>
            <tr>
              <td width="50" height="50">&nbsp;</td>
              <td width="566" height="50" align="left" valign="top">
              <table border="0" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" bordercolor="#111111" width="614" id="AutoNumber6">
                    <tbody>
                <tr>
                  <td width="144" align="right" bgcolor="#006699">&nbsp;</td>
                  <td width="470" bgcolor="#006699">
                  <p align="center"><b><font face="Arial" color="#ffff99">
                  BeeHive Login</font></b></p></td>
                </tr>
                <form name="fSubmitID" method="post" action="BH_Login.aspx">
                <tr>
                  <td width="144" align="right" bgcolor="#006699" valign="top" height="21">
                  	<p>
                  	<b><font face="Arial" color="#ffffff">&nbsp;</font></b><font face="Arial">
                    </font></p>
                  </td>
                  <td width="470" bgcolor="#ffff99" height="21">
					<p align="center">
					<b>
					<font face="Arial" color="#003399"><%=strMessage%><br>THIS SITE 
                    IS UNDER CONSTRUCTION</font></b></p></td>

                 </tr>
                 <tr>
                  <td width="144" align="right" bgcolor="#006699" valign="top" height="21">
                  	<p>
                  	<b><font face="Arial" color="#ffffff">&nbsp;</font></b><font face="Arial">
                    </font></p>
                  </td>
                  <td width="470" bgcolor="#ffff99" height="21">
                        <p align="center"><font style="BACKGROUND-COLOR: #ffff99" face="Arial" size="2">
                        PLEASE ENTER YOUR SPECIAL ACCESS CODE TO ENTER THE HIVE</font></p></td>

                 </tr>
                <tr>
                  <td width="144" align="right" bgcolor="#006699" valign="top" height="21">
                  	<b><font face="Arial" size="4" color="#ffff99">User Name:
					</font><font face="Arial" size="2" color="#ffff99">&nbsp;&nbsp;
					</font><font face="Arial" size="2" color="#ffffff">&nbsp;
					</font></b>
                  </td>
                  <td width="470" bgcolor="#006699" height="21" valign="top">
					<input name="UN" size="45"></td>

                 </tr>
                <tr>
                  <td width="144" align="right" bgcolor="#006699" valign="top" height="21">
                  	<b><font face="Arial" size="4" color="#ffff99">Password:
					</font><font face="Arial" size="2" color="#ffff99">&nbsp;&nbsp;
					</font><font face="Arial" size="2" color="#ffffff">&nbsp;
					</font></b>
                  </td>
                  <td width="470" bgcolor="#006699" height="21" valign="top">
					<input type="password" name="PW" size="45"></td>

                 </tr>
                
                <tr>
                  <td width="144" align="right" bgcolor="#006699" valign="top" height="21">
                  	&nbsp;</td>
                  <td width="470" bgcolor="#006699" height="21" valign="top">
					<p align="center">
					<a onmouseover="MM_swapImage('Image0','','img/Submit2ON.gif',1)" onmouseout="MM_swapImgRestore()" href="javascript:SubmitLoginID();"><img height="32" src="img/Submit2.gif" width="124" align="middle" border="0" name="Image0"></a></p></td>

                 </tr>



              </table>
              </td>
           </tr></form>
              </table></div>
              </td>

            </tr>
          </table>
      <div></div></td></tr></tbody></table></td></tr></table>

</body>
</html>


