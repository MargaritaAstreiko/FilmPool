import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Comment } from 'src/app/models/comment.model';
import { CommentsService } from 'src/app/services/comments.service';

@Component({
  selector: 'app-filmComments',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class FilmCommentsComponent implements OnInit {
    //@Output() comment = new EventEmitter<Comment>();
    constructor(
        private _commentsService: CommentsService
    ) {}
    
    comments!: Comment[];
    addNewComment=false;
    commentForm!: FormGroup;
    userId=localStorage.getItem("userId")||0;
    @Input() filmId!: number;


  ngOnInit() {
    this.commentForm = new FormGroup({
        commentText: new FormControl("")
      })
     this._commentsService.getComments(this.filmId).subscribe( data=>{
      this.comments=data;
     });
  }
  addComment=()=>{
    this.addNewComment=!this.addNewComment;
   // this.comment.emit(comment);
  }

  saveComment = (commentValue:any) => {
    const login = {... commentValue };

    const newComment: Comment = {
      id: 0,
      userId: +this.userId,
      filmId: this.filmId,
      createdDate: new Date(),
      comment: commentValue.commentText
    }
    this._commentsService.leaveComments(newComment).subscribe();
}
  

}

