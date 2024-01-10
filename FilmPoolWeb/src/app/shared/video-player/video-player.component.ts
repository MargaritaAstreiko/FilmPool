import { Component, Input, OnInit } from '@angular/core';
import * as Plyr from 'plyr';

@Component({
  selector: 'app-video-player',
  templateUrl: './video-player.component.html',
  styleUrls: ['./video-player.component.css']
})
export class VideoPlayerComponent implements OnInit {
  videoItems = [
      {
        name: 'Video one',
        src: 'https://cdn.plyr.io/static/demo/View_From_A_Blue_Moon_Trailer-576p.mp4',
        type: 'video/mp4'
      },
      {
        name: 'Video two',
        src: 'http://static.videogular.com/assets/videos/big_buck_bunny_720p_h264.mov',
        type: 'video/mp4'
      },
      {
        name: 'Video three',
        src: 'http://static.videogular.com/assets/videos/elephants-dream.mp4',
        type: 'video/mp4'
      }
    ];

    @Input() filmUrl!: string;
    activeIndex = 0;
    currentVideo = this.videoItems[this.activeIndex];
    data: any;
    player: any;
    src!: string;

    constructor() { }
    ngOnInit(): void {
      this.player = new Plyr('#plyrID', {  debug: true,
        volume: 0,
        autoplay: true,
        muted:false,
        loop: { active: true },
       } );
       this.player.source = {
        type: 'video',
        title: 'Example title',
        sources: [
         {
              src: this.filmUrl? this.filmUrl : this.currentVideo.src,
              type: 'video/mp4',
              size: 720
          }
        ],
      };

  };
    
    videoPlayerInit(data: any) {
      this.data = data;
      this.data.getDefaultMedia().subscriptions.loadedMetadata.subscribe(this.initVdo.bind(this));
      this.data.getDefaultMedia().subscriptions.ended.subscribe(this.nextVideo.bind(this));
    }
    nextVideo() {
      this.activeIndex++;
      if (this.activeIndex === this.videoItems.length) {
        this.activeIndex = 0;
      }
      this.currentVideo = this.videoItems[this.activeIndex];
    }
    initVdo() {
      this.data.play();
    }
    startPlaylistVdo(item: any, index: number) {
      this.activeIndex = index;
      this.currentVideo = item;
    }
}