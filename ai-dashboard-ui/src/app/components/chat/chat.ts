import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ChatService } from '../../services/chat';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-chat',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './chat.html',
  styleUrl: './chat.scss'
})
export class ChatComponent {
  message = '';
  loading = false;
  messages: { role: string; text: string }[] = [];

  constructor(
    private chatService: ChatService,
    private authService: AuthService,
    private router: Router
  ) {}

  sendMessage() {
    if (!this.message.trim()) return;

    const userMessage = this.message;
    this.messages.push({ role: 'user', text: userMessage });
    this.message = '';
    this.loading = true;

    this.chatService.sendMessage(userMessage).subscribe({
      next: (res) => {
        this.messages.push({ role: 'ai', text: res.reply });
        this.loading = false;
      },
      error: () => {
        this.messages.push({ role: 'ai', text: 'Error getting response. Please try again.' });
        this.loading = false;
      }
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  onKeyPress(event: KeyboardEvent) {
    if (event.key === 'Enter' && !event.shiftKey) {
      event.preventDefault();
      this.sendMessage();
    }
  }
}