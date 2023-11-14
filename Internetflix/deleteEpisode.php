<?php


include ('seriesStorage.php');

session_start();

$sStorage=new seriesStorage();
$serie = $sStorage->findOne(array("id" => $_GET['sid']));
$user = $_SESSION['user'];

if (!isset($_GET['sid']) || $serie==null) {
    header('location: Fooldal.php');
    exit();
}

if (!isset($_SESSION['user']) || !authorize()) {
    header('location: Fooldal.php');
    exit();
}

function authorize() {
    return isset($_SESSION["user"]) && $_SESSION["user"]['username']=="admin";
}

array_pop($serie['episodes']);

$sStorage->update($serie['id'],$serie);
header('location: Fooldal.php');
exit();

?>