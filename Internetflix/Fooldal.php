<?php
include ('seriesStorage.php');
include ('userStorage.php');

session_start();

$sStorage=new seriesStorage();
$series = $sStorage->findAll();

$uStorage=new userStorage();
$seen=[];
if (isset($_SESSION['user'])) {
    $user = $uStorage->findOne(array("username" => $_SESSION['user']['username'])); 
    foreach ($series as $serie) {
        foreach ($user['watched'] as $key => $value) {
            if ($serie['id']==$key) {
                $seen[]=$serie;
            }
        }
    }
}

function authorize() {
    return isset($_SESSION["user"]) && $_SESSION["user"]['username']=="admin";
}


?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Internetflix Főoldal</title>
    <link rel="stylesheet" href="index.css">
    
</head>
<body>
    <h1>INTERNETFLIX</h1>

    <div class="topnav">
    <?php if (!isset($_SESSION['user'])):?>
            <a href="login.php">Bejelentkezés</a>
            <a href="register.php">Regisztráció</a>
        <?php endif?>
        <?php if (isset($_SESSION['user'])):?>
            <a href="logout.php">Kijelentkezés</a>
        <?php endif?>
        <?php if (authorize()):?>
            <a href="add.php">Új sorozat felvétele</a>
        <?php endif?>
    </div>

    <div class="series">
        <h2>összes sorozat</h2><hr>
        
        <?php foreach($series as $s):?>
            <div class="serie">
                <div>
                    <img src="<?=$s['cover'] ?? ''?>" alt="">
                </div>
                <div class="seriescard">
                    <b>cím:</b> <a href="reszletezo.php?id=<?=$s['id']?>"><?=$s['title']?></a> <br>
                    <b>megjelenés éve: </b> <?=$s['year']?> <br>
                    <b>cselekmény:</b>  <?=$s['plot']?><br>
                    <b>epizódok száma:</b> <?=count($s['episodes']) ?? "0"?><br>
                    <b>legutolsó rész sugározva:</b><?=$s['episodes'][count($s['episodes'])]['date'] ?? ""?> <br>
                    <?php if (authorize()):?>
                        <a href="deleteSerie.php?id=<?=$s['id']?>">sorozat törlése</a>, <a href="modifySerie.php?id=<?=$s['id']?>">sorozat módosítása</a>
                    <?php endif?>
                </div>
            </div>  
            <hr>
        <?php endforeach?>
    </div>

    <div class="series<?=isset($_SESSION['user'])? "" : " hide"?>">
        <h2>megtekintett sorozatok</h2><hr>

    <?php foreach($seen as $s):?>
            <div class="serie">
                <div>
                    <img src="<?=$s['cover'] ?? ''?>" alt="">
                </div>
                <div class="seriescard">
                    <b>cím:</b> <a href="reszletezo.php?id=<?=$s['id']?>"><?=$s['title']?></a> <br>
                    <b>cselekmény:</b>  <?=$s['plot']?><br>
                    <b>epizódok száma:</b> <?=count($s['episodes']) ?? "0"?><br>
                    <b>legutolsó rész sugározva:</b>
                    <?=$s['episodes'][count($s['episodes'])]['date'] ?? ""?> 
                </div>
            </div>
            <hr>
        <?php endforeach?>
    </div>

</body>
</html>