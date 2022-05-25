<?php
error_reporting(0);
$api = $_GET['key'];
if(isset($api)) {
    if($api == "6456")
    {
        $off = $_POST['off'];
        $game = $_POST['game'];
        $app = $_POST['app'];
        $lock = $_POST['lock'];
        if(isset($off))
        {
            $fp = fopen('pckapat', 'w');
            fwrite($fp, $off);
            fclose($fp);
            echo "Durum : $off ";
            echo'<meta http-equiv="refresh" content="2;url=http://10.10.70.129/">';
        }
        elseif (isset($game))
        {
            $fp = fopen('oyun', 'w');
            fwrite($fp, $game);
            fclose($fp);
            echo "Durum : $game ";
            echo'<meta http-equiv="refresh" content="2;url=http://10.10.70.129/">';
        }
        elseif (isset($app))
        {
            $fp = fopen('uygulama', 'w');
            fwrite($fp, $app);
            fclose($fp);
            echo "Durum : $app";
            echo'<meta http-equiv="refresh" content="2;url=http://10.10.70.129/">';
        }
        elseif (isset($lock))
        {
            $fp = fopen('pckitle', 'w');
            fwrite($fp, $lock);
            fclose($fp);
            echo "Durum : $lock";
            echo'<meta http-equiv="refresh" content="2;url=http://10.10.70.129/">';
        }
        else{
            echo "Parametre bulunamadı.";
            return;
        }
     
    }
    else{
        echo "giris taninmadi";
    }
}

?>