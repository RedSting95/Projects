<?php
include ('seriesStorage.php');

session_start();

$user = $_SESSION['user'];

if (!isset($_GET['sid'])) {
    header('location: Fooldal.php');
    exit();
}

if (!isset($_SESSION['user']) || !authorize()) {
    header('location: Fooldal.php');
    exit();
}

$sStorage=new seriesStorage();

$serie = $sStorage->findOne(array("id" => $_GET['sid']));
$episode = $serie['episodes'][$_GET['epid']];

if ($serie==null) {
    header('location: Fooldal.php');
    exit();
}

$data=[];
$errors=[];

function authorize() {
    return isset($_SESSION["user"]) && $_SESSION["user"]['username']=="admin";
}

function validate($post,&$data,&$errors) {
    global $serie;

    $date_re="/^[0-9]{4}-[0-9]{2}-[0-9]{2}$/i";

    if(!isset($post['title']) || trim($post['title'])==='') {
        $errors['title'] = "Nem adtál meg címet!";
    } else {
        $data['title']=$post['title'];
    }

    if(!isset($post['date']) || trim($post['date'])==='') {
        $errors['date'] = "Nem adtad meg a megjelenés idejét!";
    } else if (preg_match($date_re,$post['date'])!==1) {
        $errors['date'] = "Rossz formátumban adtad meg a dátumot!";
    } else {
        $data['date']=$post['date'];
    }

    if(!isset($post['plot']) || trim($post['plot'])==='') {
        $errors['plot'] = "Nem adtad meg a cselekményt!";
    } else {
        $data['plot']=$post['plot'];
    }

    if(!isset($post['rating']) || trim($post['rating'])==='') {
        $errors['rating'] = "Nem adtál meg értékelést!";
    } else if (!filter_var($post['rating'],FILTER_VALIDATE_FLOAT)) {
        $errors['rating'] = "Rosszul adtad meg az értékelést!";
    } else if ($post['rating'] > 10 || $post['rating'] < 0) {
        $errors['rating'] = "Az értékelésnek 0, és 10 közé kell esnie!";
    } else {
        $data['rating']=$post['rating'];
    }

    $data['id']=$_GET['epid'];

    return count($errors)===0;
}

if (count($_POST)!==0) {
    if (validate($_POST,$data,$errors)) {
        $serie['episodes'][$data['id']]=$data;
        $sStorage->update($serie['id'],$serie);
        header('location: Fooldal.php');
        exit();
    }
}

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Internetflix epizód módosítása</title>
    <link rel="stylesheet" href="index.css">
</head>
<body>
    <h1>INTERNETFLIX - epizód módosítása</h1>

    <div class="topnav">
        <a href="Fooldal.php">Főoldal</a>
        <a href="logout.php">Kijelentkezés</a>
    </div>

    <form action="" method="post" class="form" novalidate>
        Epizód címe: <input type="text" name="title" id="" value="<?=$_POST['title']?? $episode['title'] ?? ''?>"><span><?=$errors['title'] ?? ''?></span><br>
        Megjelenés ideje: <input type="text" name="date" id="" value="<?=$_POST['date'] ?? $episode['date'] ?? ''?>"><span><?=$errors['date'] ?? ''?></span><br>
        Cselekmény: <input type="text" name="plot" id="" value="<?=$_POST['plot'] ?? $episode['plot'] ?? ''?>"><span><?=$errors['plot'] ?? ''?></span><br>
        Értékelés: <input type="number" step="0.1" name="rating" id="" value="<?=$_POST['rating'] ?? $episode['rating'] ?? ''?>"><span><?=$errors['rating'] ?? ''?></span><br>
        <button>Módosít</button>
    </form>
</body>
</html>