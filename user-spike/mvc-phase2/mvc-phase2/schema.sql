CREATE TABLE `notes` (
  `note_id` bigint(20) NOT NULL auto_increment,
  `dt_added` datetime NOT NULL,
  `dt_modified` datetime NOT NULL,
  `subject` varchar(255) NOT NULL,
  `body` longtext NOT NULL,
  PRIMARY KEY  (`note_id`)
) ENGINE=MyISAM  DEFAULT CHARSET=latin1;
