import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { take } from 'rxjs';
import { Post } from 'src/app/_models/post';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { PostsService } from 'src/app/_services/posts.service';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrls: ['./post-detail.component.css']
})
export class PostDetailComponent implements OnInit {
  post: Post | undefined;
  user : User | null = null;
  isEditable: boolean = false;
  constructor(private postService: PostsService,private accountService: AccountService, private route: ActivatedRoute) { 
    this.loadPost();
    if (this.post)
      console.log('UserID checking2 : ' + this.post?.id);

  }
  DoCheck(): void{
    if (this.user?.username == this.post?.userName) {
      console.log('test: ' + this.user?.username.valueOf());
      console.log(this.post?.userName);
      this.isEditable = true;
    }
  }
  ngOnInit(): void {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if(user) {
          this.user = user;
          console.log('Known as : ' + this.user.knownAs);
          console.log('Title: ' + this.post?.title);
        }
      }
    })
  }


  loadPost() {
    const postid = this.route.snapshot.paramMap.get('id');
    var postId: number = Number(postid);
    this.postService.getPostById(postId).subscribe({
      next: post => {
        if (post) {
          this.post = post;
        }
      }
    });
  }
}
