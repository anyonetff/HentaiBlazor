-- 写入导航数据.

DELETE FROM s_function;

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('10', '0', true, '/', '歓迎', 'home', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('20', '0', true, '/anime/index', '18禁アニメ', 'play-square', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('30', '0', true, '/comic/index', '成年コミック', 'read', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);


INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('60', '0', false, '', 'コンテンツ', 'appstore', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('6010', '60', true, '/basic/catalog/list', 'フォルダ', 'folder', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('6020', '60', true, '/anime/video/list', 'アニメ管理', 'video-camera', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('6030', '60', true, '/comic/book/list', 'コミック管理', 'book', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('6060', '60', true, '/basic/author/list', '著者', 'team', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('6070', '60', true, '/basic/tag/list', 'タグ', 'tags', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('80', '0', false, '', 'システム', 'setting', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('8010', '80', true, '/security/function/list', 'メニュー', 'bars', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO s_function (f_id, f_parent, f_leaf, f_path, f_name, f_icon, f_note, x_insert_, x_update_)
VALUES ('8050', '80', true, '/security/user/list', 'ユーザー', 'idcard', '', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

-- 写入标签数据

INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.futanari', 'ふたなり', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.yuri', '百合', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.ahegao', 'あへ顔', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.netorare', '寝取る', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.elf', 'エルフ', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
INSERT INTO b_tag (t_id, t_name, t_alias, t_items, t_note, x_insert_, x_update_)
VALUES ('.succubus', 'サキュバス', '.', 0, '预设分类', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
