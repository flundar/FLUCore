<?php
$kul[0]["kullanici"] = "admin";
$kul[0]["sifre"] = "erdem34!!";

function authenticate()
{
    header(
        'WWW-Authenticate: Basic realm="KULLANICI ADINIZI VE SiFRENiZi GiRiN."'
    );
    header("HTTP/1.0 401 Unauthorized");
    echo '<br/><br/><br/><b><body bgcolor=#29a2d6><font color=white size=2 face="Trebuchet MS"><center>GiRiS YAPILMADI !<br/><br/>Bu sayfaya erisim sinirlidir. Lutfen kullanici adini ve sifreyi girin.<br/><br/><br/><a href="index.php"><font color=white size=2 face="Trebuchet MS">Tekrar denemek icin TIKLAYIN</font></a><br/><br/><a href="index.php"><font color=white size=2 face="Trebuchet MS"></font></a></b>';
    exit();
}
if (!isset($_SERVER["PHP_AUTH_USER"]) || !isset($_SERVER["PHP_AUTH_PW"])) {
    authenticate();
} else {
    for ($i = 0; $i < count($kul); $i++) {
        if (
            $_SERVER["PHP_AUTH_USER"] == $kul[$i]["kullanici"] &&
            $_SERVER["PHP_AUTH_PW"] == $kul[$i]["sifre"]
        ) {
            $auth = true;
        }
    }
    if ($auth != true) {
        authenticate();
    }
}
?>

<html>
    <meta http-equiv="content-type" content="text/html; charset=utf-8">
    <header>
        <title>Yönetim Paneli</title>
    </header>
    <body>
        <form name="Choose" action="ogretmenpost.php?key=6456" method="POST">
            <input type="radio" name="lock" value="true" /> Bilgisayarları Kitle
            <input type="radio" name="lock" value="false" /> Bilgisayarları Kitlemeyi İptal Et
            <hr>
            <input type="radio" name="off" value="true" /> Bilgisayarları Kapat
            <input type="radio" name="off" value="false" /> Bilgisayarları Kapatmayı İptal Et
            <hr>
            <input type="radio" name="game" value="true" /> Oyun Korumasını Aç
            <input type="radio" name="game" value="false" />Oyun Korumasını Kapat
            <hr>
            <input type="radio" name="app" value="true" /> Uygulama Korumasını Aç
            <input type="radio" name="app" value="false" /> Uygulama Korumasını Kapat
            <hr>
            <input type="submit" name="submit" value="Gonder" />
        </form>
    </body>

</html>
