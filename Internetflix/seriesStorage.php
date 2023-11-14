<?php
include_once('storage.php');

class seriesStorage extends Storage {
    public function __construct() {
        parent::__construct(new JsonIO('series.json'));
    }
}

?>