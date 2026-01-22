import { defineStore } from 'pinia'
import type { Note, CreateNoteDto, UpdateNoteDto } from '@/types'
import { fetchNotesApi, createNoteApi, updateNoteApi, deleteNoteApi, getNoteByIdApi } from '@/composables/useNotes'

export const useNotesStore = defineStore('notes', {
  state: () => ({
    notes: [] as Note[],
    loading: false,
    error: '',
    searchQuery: '',
    sortBy: 'date' as 'date' | 'title',
    sortOrder: 'desc' as 'asc' | 'desc'
  }),
  getters: {
    filteredNotes: (state) => {
      let filtered = state.notes.filter(note =>
        note.title.toLowerCase().includes(state.searchQuery.toLowerCase()) ||
        note.content.toLowerCase().includes(state.searchQuery.toLowerCase())
      );
      const sortKey = state.sortBy === 'date' ? 'createdAt' : 'title';
      filtered.sort((a, b) => {
        let aVal: any = a[sortKey];
        let bVal: any = b[sortKey];
        if (sortKey === 'createdAt') {
          aVal = new Date(aVal);
          bVal = new Date(bVal);
        }
        if (aVal < bVal) return state.sortOrder === 'asc' ? -1 : 1;
        if (aVal > bVal) return state.sortOrder === 'asc' ? 1 : -1;
        return 0;
      });
      return filtered;
    }
  },
  actions: {
    async fetchNotes() {
      this.loading = true
      this.error = ''
      try {
        this.notes = await fetchNotesApi()
      } catch (error) {
        this.error = error instanceof Error ? error.message : 'Failed to fetch notes'
      } finally {
        this.loading = false
      }
    },
    async createNote(dto: CreateNoteDto) {
      this.loading = true
      this.error = ''
      try {
        const newNote = await createNoteApi(dto)
        this.notes.push(newNote)
      } catch (error) {
        this.error = error instanceof Error ? error.message : 'Failed to create note'
      } finally {
        this.loading = false
      }
    },
    async updateNote(id: string, dto: UpdateNoteDto) {
      this.loading = true
      this.error = ''
      try {
        const updatedNote = await updateNoteApi(id, dto)
        const index = this.notes.findIndex(note => note.id === id)
        if (index !== -1) {
          this.notes[index] = updatedNote
        }
      } catch (error) {
        this.error = error instanceof Error ? error.message : 'Failed to update note'
      } finally {
        this.loading = false
      }
    },
    async deleteNote(id: string) {
      this.loading = true
      this.error = ''
      try {
        await deleteNoteApi(id)
        this.notes = this.notes.filter(note => note.id !== id)
      } catch (error) {
        this.error = error instanceof Error ? error.message : 'Failed to delete note'
      } finally {
        this.loading = false
      }
    },
    getNoteById(id: string): Note | undefined {
      return this.notes.find(note => note.id === id)
    }
  }
})