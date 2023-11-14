<?php

session_start();

unset($_SESSION['user']);
header('location: Fooldal.php');
exit();

?>