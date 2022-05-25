<?php

// configuration

$sistem = $_GET['s'];
$url = 'http://10.10.70.129/cko.php';

if (isset($sistem))
{
    if ($sistem == "u")
    {
        $sistem = "./uygulamaveri";
    }
    elseif ($sistem == "o")
    {
        $sistem = "./oyunveri.json";
    }

}
// check if form has been submitted
if (isset($_POST['text']))
{
    // save the text contents
    file_put_contents($sistem, $_POST['text']);

    // redirect to form again
    header(sprintf('Location: %s', $url));
    printf('<a href="%s">Moved</a>.', htmlspecialchars($url));
    exit();
}

// read the textfile
$text = file_get_contents($sistem);

?>
<!-- HTML form -->
<form action="" method="post">
<textarea name="text"><?php echo htmlspecialchars($text) ?></textarea>
<input type="submit" />
<input type="reset" />
</form>