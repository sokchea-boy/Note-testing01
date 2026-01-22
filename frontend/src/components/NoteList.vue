<template>
  <div class="mb-6 flex flex-col sm:flex-row gap-4">
    <input
      v-model="notesStore.searchQuery"
      type="text"
      placeholder="Search notes..."
      class="flex-1 px-4 py-2 border border-gray-300 rounded-md focus:ring-indigo-500 focus:border-indigo-500"
    />
    <select
      v-model="notesStore.sortBy"
      class="px-4 py-2 border border-gray-300 rounded-md focus:ring-indigo-500 focus:border-indigo-500"
    >
      <option value="date">Date</option>
      <option value="title">Title</option>
    </select>
    <select
      v-model="notesStore.sortOrder"
      class="px-4 py-2 border border-gray-300 rounded-md focus:ring-indigo-500 focus:border-indigo-500"
    >
      <option value="asc">Ascending</option>
      <option value="desc">Descending</option>
    </select>
  </div>
  <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
    <NoteCard v-for="note in notesStore.filteredNotes" :key="note.id" :note="note" />
  </div>
</template>

<script setup lang="ts">
import NoteCard from './NoteCard.vue'
import { useNotesStore } from '@/stores/notesStore'

const notesStore = useNotesStore()
</script>