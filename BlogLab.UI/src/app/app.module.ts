import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {TooltipModule} from 'ngx-bootstrap/tooltip';
import { SummaryPipe } from './pipes/summary.pipe';
import { BlogComponent } from './components/blog-components/blog/blog.component';
import { BlogCardComponent } from './components/blog-components/blog-card/blog-card.component';
import { BlogEditComponent } from './components/blog-components/blog-edit/blog-edit.component';
import { BlogsComponent } from './components/blog-components/blogs/blogs.component';
import { FamousBlogsComponent } from './components/blog-components/famous-blogs/famous-blogs.component';
import { CommentBoxComponent } from './components/comments-components/comment-box/comment-box.component';
import { CommentSystemComponent } from './components/comments-components/comment-system/comment-system.component';
import { CommentsComponent } from './components/comments-components/comments/comments.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { NotfoundComponent } from './components/notfound/notfound.component';
import { PhotoAlbumComponent } from './components/photo-album/photo-album.component';
import { RegisterComponent } from './components/register/register.component';


@NgModule({
  declarations: [
  
    SummaryPipe,
       BlogComponent,
       BlogCardComponent,
       BlogEditComponent,
       BlogsComponent,
       FamousBlogsComponent,
       CommentBoxComponent,
       CommentSystemComponent,
       CommentsComponent,
       DashboardComponent,
       HomeComponent,
       LoginComponent,
       NavbarComponent,
       NotfoundComponent,
       PhotoAlbumComponent,
       RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
     TooltipModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
