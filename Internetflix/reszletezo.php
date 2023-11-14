<?php
include ('seriesStorage.php');
include ('userStorage.php');

session_start();

$sStorage=new seriesStorage();
$serie = $sStorage->findById($_GET['id']);

if($serie==null) {
    header("Location: Fooldal.php");
    exit();
}

$uStorage=new userStorage();
if (isset($_SESSION['user'])) {
    $user = $uStorage->findOne(array("username" => $_SESSION['user']['username'])); 
}

function watchGen($serie,$user,$epid) {
    if (isset($user['watched'][$serie['id']]) && $user['watched'][$serie['id']]+1==$epid || !isset($user['watched'][$serie['id']]) && $epid==1) { 
        print_r('<form action="" method="post"><input type="checkbox" value="checked" name="seen"><button>megtekintés</button></form>');
    }
}

function isWatched($serie,$user,$epid) {
    if (isset($user['watched'][$serie['id']]) && $user['watched'][$serie['id']]>=$epid) { 
        return ' watched';
    }
    return '';
}

function authorize() {
    return isset($_SESSION["user"]) && $_SESSION["user"]['username']=="admin";
}

if ($_POST!==0) {
    if (isset($_POST['seen'])) {
        if (!isset($user['watched'][$serie['id']])) {
            $user['watched'][$serie['id']]=0;
        }
        $user['watched'][$serie['id']]++;
        $uStorage->update($user['id'],$user);
    } 
}

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Internetflix részletező</title>
    <link rel="stylesheet" href="index.css">
</head>
<body>
    <h1>INTERNETFLIX - részletező</h1>
    <div class="topnav">
        <a href="Fooldal.php">Főoldal</a>
        <?php if (!isset($_SESSION['user'])):?>
            <a href="login.php">Bejelentkezés</a>
            <a href="register.php">Regisztráció</a>
        <?php endif?>
        <?php if (isset($_SESSION['user'])):?>
            <a href="logout.php">Kijelentkezés</a>
        <?php endif?>
        <?php if (authorize()):?>
            <a href="addEpisode.php?id=<?=$_GET['id']?>">Új epizód hozzáadása</a>
        <?php endif?>
    </div>

    <div class="series">
                <h2><?=$serie['title']?></h2><hr>
                <div class="serie">
                    <div>
                        <img src="<?=$serie['cover'] ?? ""?>" alt="">
                    </div>
                    <div class="seriescard">
                        <b>cím:</b> <?=$serie['title']?> <br>
                        <b>megjelenés éve: </b> <?=$serie['year']?> <br>
                        <b>cselekmény:</b>  <?=$serie['plot']?><br>
                        <b>epizódik száma:</b> <?=count($serie['episodes']) ?? "0"?><br>
                        <b>legutolsó rész sugározva:</b>
                        <?=$serie['episodes'][count($serie['episodes'])]['date'] ?? ""?>
                    </div>
                </div>
                
    </div>

    <div class="series">
        <h2>Epizódok</h2><hr>

        <?php foreach($serie['episodes'] as $episode):?>
            <div class="serie
            <?php if(isset($_SESSION['user'])):?>
                <?=isWatched($serie,$user,$episode['id'])?>
            <?php endif?>
            " >
                <div class="seriescard">
                    <b><?=$episode['id']?>.rész</b><br>
                    <b>cím:</b> <?=$episode['title']?> <br>
                    <b>cselekmény:</b>  <?=$episode['plot']?><br>
                    <b>rész sugározva:</b> <?=$episode['date']?><br>
                    <b>értékelés:</b> <?=$episode['rating']?>
                    <?php if (authorize()):?>
                        <br><a href="modifyEpisode.php?sid=<?=$_GET['id']?>&epid=<?=$episode['id']?>">epizód módosítása</a>
                        <?php if (count($serie['episodes'])==$episode['id']):?>
                            , <a href="deleteEpisode.php?sid=<?=$_GET['id']?>&epid=<?=$episode['id']?>">utolsó epizód törlése</a>
                        <?php endif?>
                    <?php endif?>

                    <?php if(isset($_SESSION['user'])):?>
                        <b><?=watchGen($serie,$user,$episode['id'])?></b>
                    <?php endif?>
                </div>
            </div>
            <hr>
        <?php endforeach?>
    </div>
</body>
</html>