<?php
include ('seriesStorage.php');

$sStorage=new seriesStorage();

$data=[];
$errors=[];

function validate($post,&$data,&$errors) {
    if(!isset($post['title']) || trim($post['title'])==='') {
        $errors['title'] = "Nem adtál meg címet!";
    } else {
        $data['title']=$post['title'];
    }

    if(!isset($post['year']) || trim($post['year'])==='') {
        $errors['year'] = "Nem adtad meg a megjelenés évét!";
    } else if (!filter_var($post['year'],FILTER_VALIDATE_INT)) {
        $errors['year'] = "Rossz formátumban adtad meg a megjelenés évét!";
    } else if ($post['year']>2022 || $post['year']<1900) {
        $errors['year'] = "A megjelenés éve 1900-2022-es intervallumban adható meg!";
    } else {
        $data['year'] = $post['year'];
    }

    if(!isset($post['plot']) || trim($post['plot'])==='') {
        $errors['plot'] = "Nem adtad meg a cselekményt!";
    } else {
        $data['plot']=$post['plot'];
    }

    if(isset($post['cover']) && trim($post['cover'])!=='') {
        if (!filter_var($post['cover'],FILTER_VALIDATE_URL)) {
            $errors['cover'] = "Rossz formátumban adtad meg a poszter url-jét!";
        } else {
            $data['cover']=$post['cover'];
        }
    } else {
        $data['cover']="";
    }

    $data['episodes']=[];

    return count($errors)===0;

}

if (count($_POST)!==0) {
    if (validate($_POST,$data,$errors)) {
        $serie = $data;
        $sStorage->add($serie);
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
    <title>Internetflix új sorozat hozzáadása</title>
    <link rel="stylesheet" href="index.css">
</head>
<body>
    <h1>INTERNETFLIX - új sorozat</h1>

    <div class="topnav">
        <a href="Fooldal.php">Főoldal</a>
        <a href="logout.php">Kijelentkezés</a>
    </div>

    <form action="" method="post" class="form" novalidate>
        Sorozat címe: <input type="text" name="title" id="" value="<?=$_POST['title'] ?? ''?>"><span><?=$errors['title'] ?? ''?></span><br>
        Megjelenés éve: <input type="number" name="year" id="" value="<?=$_POST['year'] ?? ''?>"><span><?=$errors['year'] ?? ''?></span><br>
        Cselekmény: <input type="text" name="plot" id="" value="<?=$_POST['plot'] ?? ''?>"><span><?=$errors['plot'] ?? ''?></span><br>
        Poszter: <input type="url" name="cover" id="" value="<?=$_POST['cover'] ?? ''?>"><span><?=$errors['cover'] ?? ''?></span><br>
        <button>Hozzáad</button>
    </form>
</body>
</html>