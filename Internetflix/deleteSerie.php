<?php
include ('seriesStorage.php');

session_start();

$sStorage=new seriesStorage();
$serie = $sStorage->findOne(array("id" => $_GET['id']));

$user = $_SESSION['user'];

if (!isset($_GET['id']) || $serie==null) {
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


$sStorage->delete($serie['id']);
header('location: Fooldal.php');
exit();

?>