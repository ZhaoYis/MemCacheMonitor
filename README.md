# MemCacheMonitor

项目提供了一个MemCache的监控程序，网上提供的大部分都是Php写的工具（memadmin 是一款不错的监控工具），需要手动部署，操作不是很方便，因此有了这一款桌面程序，感谢博客园小伙伴（晓风拂月）提供源码，文章地址：http://www.cnblogs.com/xffy1028/archive/2013/02/01/2861706.html，
我在此基础上做了一些小的小调整，修复了一些Bug。

### 项目基本实现的功能：
1、显示MemCache 基本信息<br>
2、监控统计MemCache 信<br>
3、对MemCache 的区块进行统计<br>
4、MemCache 中的数据维护<br>
5、计算各个服务器的命中率<br>
6、对服务器列表的监控<br>
7、服务器数据备份<br>
8、服务器数据还原<br>
9、添加服务器<br>
### 基本信息
显示Memcache启动以来的基本数据信息，通过选择不同的服务器，切换显示不同的服务器当前的基本信息：<br>
![基本信息](https://github.com/ZhaoYis/MemCacheMonitor/blob/master/1.png)
### 添加服务器
1)点击工具，选择添加服务器；<br>
2)在弹出的窗体中，输入服务器IP，端口保存；<br>
3)保存时将对要保存的服务器进行一次验证，如果不能正常连接，则无法保存；<br>
4)添加时,同一台服务器不可以多次添加,会进行验证；<br>
5)双击列表中的一行数据,可以对数据进行编辑,也可以删除当前服务器；<br>
6)点击重置回到添加状态。<br>
![添加服务器](https://github.com/ZhaoYis/MemCacheMonitor/blob/master/1-1.png)
### 统计监控
1)选择要监控的服务器；<br>
2)输入刷新频率(只能是数字)，点击启动监控，即可对选中服务器的信息进行时时监控；<br>
3)监控是禁止切换服务器，可以切换选项卡，查询其他选项卡信息；<br>
4)点击停止监控后，一个监控周期结束，可以正常切换服务器进行重新监控。<br>
![统计监控](https://github.com/ZhaoYis/MemCacheMonitor/blob/master/2.png)
### 区块查询
1)选择要查询的服务器；<br>
2)选择区块后可以统计每个区块的数据总量；<br>
3)输入要查询数据量，可以对不同的区块的数据进行查询（注：尽量不要查询太多的数据，否则可能会影响性能）；<br>
4)双击列表中查询的数据，可以查询每个Key对应的详细信息；<br>
5)详细信息页支持上一页下一页，支持查找替换，esc关闭窗体；<br>
6)程序自动识别如果是json数据，可以点击格式化，更清晰的查看数据结构；<br>
7)点击格式化以后，可以撤销格式；<br>
8)点击删除，从选中服务器删除当前数据；<br>
9)点击保存，保存该数据到当前选中服务器。<br>
![区块查询](https://github.com/ZhaoYis/MemCacheMonitor/blob/master/3.png)<br>
![区块查询](https://github.com/ZhaoYis/MemCacheMonitor/blob/master/3.png)<br>
### 数据维护
1)选择要查询的Memcache 服务器；<br>
2)输入要查询的key,多个key ,请使用半角逗号分割；<br>
3)双击列表中查询的数据，可以查询每个Key对应的详细信息；<br>
4)详细信息页支持上一页下一页，支持查找替换，esc关闭窗体；<br>
5)程序自动识别如果是json数据，可以点击格式化，更清晰的查看数据结构；<br>
6)点击格式化以后，可以撤销格式；<br>
7)点击删除，从选中服务器删除当前数据；<br>
8)点击保存，重新保存该数据；<br>
9)单击每一个cell 单元格，进入编辑状态，可以右键复制数据；<br>
10)新建数据，输入key 和value 点击提交，会把数据提交到当前选定的服务器。<br>
![数据维护](https://github.com/ZhaoYis/MemCacheMonitor/blob/master/4.png)
### 命中率查询
1)选择要查询的服务器（切换服务器以对不同服务器的数据进行统计）；<br>
2)对服务器中的Get命令，Delete命令，INCR命令，DECR命令，以及CAS命令进行统计，并计算各种命令的命中率。<br>
![命中率查询](https://github.com/ZhaoYis/MemCacheMonitor/blob/master/5.png)
### 服务器监控列表
列出当前所有正在监控服务器的部分详细信息。<br>
![服务器监控列表](https://github.com/ZhaoYis/MemCacheMonitor/blob/master/6.png)

