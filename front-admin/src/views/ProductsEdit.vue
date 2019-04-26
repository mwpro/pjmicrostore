<template>
  <div>
    <div
      class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom"
    >
      <h2>{{ isEditMode ? "Edytuj" : "Dodaj" }} produkt</h2>
    </div>
    <b-form @submit="onSubmit" v-if="show">
      <b-form-group id="input-group-1" label="Nazwa produktu:" label-for="input-1">
        <b-form-input id="input-1" v-model="form.name" required></b-form-input>
      </b-form-group>

      <b-form-group id="input-group-2" label="Cena:" label-for="input-2">
        <b-form-input id="input-2" type="number" v-model="form.price" required></b-form-input>
      </b-form-group>

      <b-form-group id="input-group-3" label="Kategoria:" label-for="input-3">
        <b-form-select id="input-3" v-model="form.categoryId" :options="categories" required></b-form-select>
      </b-form-group>

      <b-form-group id="input-group-3" label="Opis:" label-for="input-3">
        <b-form-textarea id="textarea" v-model="form.description" rows="3" max-rows="6"></b-form-textarea>
      </b-form-group>

      <b-form-group id="input-group-3" label="Dostępny w sprzedaży:" label-for="input-3">
        <b-form-checkbox v-model="form.isActive" name="check-button" switch></b-form-checkbox>
      </b-form-group>

      <h3>Atrybuty</h3>
      <!-- TODO skip already added attributes -->
      <!-- TODO allow adding new attributes -->
      <b-form-group
        id="input-group-3"
        label-for="input-3"
        label-cols-sm="3"
        label="Dodaj nowy atrybut"
      >
        <b-input-group>
          <b-form-select id="input-3" v-model="attributeToAdd" :options="attributes"></b-form-select>
          <b-input-group-append>
            <b-button @click="addAttribute()">Dodaj</b-button>
          </b-input-group-append>
        </b-input-group>
      </b-form-group>

      <br />

      <b-form-group
        id="input-group-3"
        label-for="input-3"
        label-cols-sm="3"
        v-for="attribute in form.attributes"
        :key="attribute.attributeId"
        :label="attributes.find(a => a.value == attribute.attributeId).text"
      >
        <b-input-group>
          <b-form-input id="input-1" v-model="attribute.attributeValue" required></b-form-input>
          <b-input-group-append>
            <b-button @click="removeAttribute(attribute)">Usuń</b-button>
          </b-input-group-append>
        </b-input-group>
      </b-form-group>

      <h3>Zdjęcia</h3>
      <b-form-file
        v-model="photosToUpload"
        placeholder="Wybierz pliki"
        drop-placeholder="Przeciągnij pliki tutaj..."
        browse-text="Przeglądaj"
        multiple
        accept="image/*"
      ></b-form-file>
      <b-container fluid class="p-4 bg-dark">
        <b-row>
          <b-col v-for="photo in photos" v-bind:key="photo.photoId">
            <b-img thumbnail fluid :src="photo.originalUrl" alt="Image 1"></b-img>
          </b-col>
        </b-row>
      </b-container>

      <b-button type="submit" variant="primary">Zapisz</b-button>
    </b-form>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      attributeToAdd: null,
      photosToUpload: null,
      photos: [],
      form: {
        name: '',
        price: 0,
        description: '',
        categoryId: null,
        attributes: [],
        isActive: true,
      },
      show: true,
    };
  },
  props: {
    productId: String, // todo prop type
  },
  methods: {
    onSubmit(evt) {
      evt.preventDefault();
      this.$store
        .dispatch(
          this.isEditMode
            ? 'products/updateProductAction'
            : 'products/saveProductAction',
          this.form,
        )
        .then((result) => {
          if (this.photosToUpload) {
            const url = `/api/products/${result.id}/photos`;
            const formData = new FormData();
            this.photosToUpload.forEach((x) => {
              formData.append('photos', x, x.name);
            });

            return axios
              .post(url, formData) // https://github.com/axios/axios/blob/master/examples/upload/index.html#L29-L33
              .then((response) => {
                if (response.status !== 202) throw Error(response.message);
                this.$router.push({ name: 'products' });
              });
            // TODO .catch(captains.error);
            // eslint-disable-next-line no-else-return
          } else {
            this.$router.push({ name: 'products' });
          }
        })
        .catch((error) => {
          // todo
        });
    },
    addAttribute() {
      this.form.attributes.push({
        attributeId: this.attributeToAdd,
        attributeValue: null,
      });
    },
    removeAttribute(attribute) {
      this.form.attributes.splice(this.form.attributes.indexOf(attribute), 1);
    },
  },
  computed: {
    categories() {
      const treePrefix = '-';
      const flattenCategories = function flattenCategories(categories, prefix) {
        return categories.reduce((acc, val) => {
          acc.push({ text: `${prefix}${val.name}`, value: val.id });
          return acc.concat(flattenCategories(val.child, prefix + treePrefix));
        }, []);
      };

      return flattenCategories(this.$store.state.products.categoriesList, '');
    },
    attributes() {
      return this.$store.state.products.attributesList.map(attr => ({
        text: attr.name,
        value: attr.id,
      }));
    },
    isEditMode() {
      return this.productId !== undefined;
    },
  },
  created() {
    this.$store.dispatch('products/getCategoriesAction');
    this.$store.dispatch('products/getAttributesAction');
    if (this.isEditMode) {
      this.$store
        .dispatch('products/getProductAction', this.productId)
        .then((result) => {
          this.form.productId = result.id;
          this.form.name = result.name;
          this.form.price = result.price;
          this.form.description = result.description;
          this.form.categoryId = result.categoryId;
          this.form.isActive = result.isActive;
          this.form.attributes = [];
          result.attributes.forEach((attr) => {
            this.form.attributes.push({
              attributeId: attr.attributeId,
              attributeValue: attr.value,
            });
          });
        })
        .catch((error) => {
          // todo
        });

      this.$store.dispatch('products/getPhotosAction', this.productId)
        .then((result) => {
          this.photos = result;
        })
        .catch((error) => {
          // todo
        });
    }
  },
};
</script>

<style>
</style>
