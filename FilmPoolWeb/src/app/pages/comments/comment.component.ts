import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Comment } from 'src/app/models/comment.model';
import { CommentCreateModel } from 'src/app/models/commentcreate.model';
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
  ) { }

  comments!: Comment[];
  addNewComment = false;
  editComment = false;
  commentText!: string;
  commentForm!: FormGroup;
  commentTextNew!: string;
  commentId!: number;
  userId = localStorage.getItem("userId") || 0;
  @Input() filmId!: number;


  ngOnInit() {
    this._commentsService.getComments(this.filmId).subscribe(data => {
      this.comments = data.map(i => {
        i.picture = i.picture?.length > 0 ? `data:image/jpg;base64,${i.picture}` : "/assets/nofilm.png";
        return i
      });
    });
  }

  addComment = () => {
    this.addNewComment = !this.addNewComment;
  }

  deleteComment = (id: number) => {
    this._commentsService.removeComment(id).subscribe();
    this._commentsService.getComments(this.filmId).subscribe(data => {
      this.comments = data.map(i => {
        i.picture = i.picture?.length > 0 ? `data:image/jpg;base64,${i.picture}` : "/assets/nofilm.png";
        return i
      });
    });
  }

  changeComment = (id: number) => {
    this.editComment = !this.editComment
    this.commentId = id;
  }
 
  updateComment =(text: string)=>{
    this.editComment = !this.editComment;
    this._commentsService.updateComments(this.commentId, +this.userId, text, new Date()).subscribe();
    this.commentId=0;

  }
  saveComment = () => {
    this.addComment();
    const newComment: CommentCreateModel = {
      id: 0,
      userId: +this.userId,
      filmId: this.filmId,
      createdDate: new Date(),
      comment: this.commentText,
    }
    this._commentsService.leaveComments(newComment).subscribe();
  }


}

