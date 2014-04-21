BingWallpaper
======

桌面壁纸更换成Bing.com的每日图片

# Bing.com接口
## 接口地址：

http://www.bing.com/HPImageArchive.aspx?format=xml&idx=1&n=1&mkt=en-US

## 参数说明:

- format：接口返回格式，已知可选项xml,js
- idx:日期表示0为当天，-1为明天，1为昨天，2为后天，依次类推，已知可选项-1 ~ 18
- n:未知含义
- mkt:地区编号(可选项)，不同地区会获得不同壁纸。已知可选项en-US, zh-CN, ja-JP, en-AU, de-DE, en-NZ, en-CA

## 图片分辨率
解析回返报文后可拼接所需的图片分辨率，
已知可选项640x480,800x600,1024x768,1280x720,1920x1080,800x480,1366x768,1920x1200,1280x768

## 分辨率长宽比
640x480		1.333333333
800x600		1.333333333
1024x768	1.333333333
1280x720	1.777777778
1920x1080	1.777777778
800x480		1.666666667
1366x768	1.778645833
1920x1200	1.6
1280x768	1.666666667

## XML返回报文示例

```xml
<images>
	<image>
		<startdate>20140419</startdate>
		<fullstartdate>201404190000</fullstartdate>
		<enddate>20140420</enddate>
		<url>
			/az/hprichbg/rb/SecondBeach_EN-US12941316196_1366x768.jpg
		</url>
		<urlBase>/az/hprichbg/rb/SecondBeach_EN-US12941316196</urlBase>
		<copyright>
			Second Beach, near Olympic National Park and La Push, Washington (© Ian Shive/Tandem)
		</copyright>
		<copyrightlink>
			http://www.bing.com/search?q=La+Push+Beach&form=hpcapt
		</copyrightlink>
		<drk>1</drk>
		<top>1</top>
		<bot>1</bot>
		<hotspots>
			<hotspot>
				<desc>
					Live near this beach and you're in the westernmost zip code in the contiguous United States.
				</desc>
				<link>
					http://www.bing.com/images/search?q=La+Push,+Washington&FORM=hphot1
				</link>
				<query>Welcome to 98350</query>
				<LocX>19</LocX>
				<LocY>39</LocY>
			</hotspot>
			<hotspot>
				<desc>If this wilderness could speak…</desc>
				<link>
					http://www.bing.com/videos/search?q=If+Wilderness+Could+Speak&FORM=Hphot2#view=detail&mid=C215617312C0E109C780C215617312C0E109C780
				</link>
				<query>What would it say?</query>
				<LocX>32</LocX>
				<LocY>43</LocY>
			</hotspot>
			<hotspot>
				<desc>
					An 18-century English explorer thought the nearby mountain peaks rivaled the mountain home of Greek gods.
				</desc>
				<link>
					http://www.bing.com/search?q=Olympic+Mountains&form=hphot3
				</link>
				<query>The idea took hold and lent the range its name</query>
				<LocX>65</LocX>
				<LocY>35</LocY>
			</hotspot>
			<hotspot>
				<desc>
					When you step out onto this beach, you're entering the territory…
				</desc>
				<link>
					http://www.bing.com/search?q=Quileute+people&form=hphot4
				</link>
				<query>
					Of a people long renowned for their fishing and boat-building
				</query>
				<LocX>82</LocX>
				<LocY>49</LocY>
			</hotspot>
		</hotspots>
		<messages/>
	</image>
	<tooltips>
		<loadMessage>
			<message>正在加载...</message>
		</loadMessage>
		<previousImage>
			<text>上一页</text>
		</previousImage>
		<nextImage>
			<text>下一页</text>
		</nextImage>
		<play>
			<text>播放</text>
		</play>
		<pause>
			<text>暂停</text>
		</pause>
	</tooltips>
</images>
```
